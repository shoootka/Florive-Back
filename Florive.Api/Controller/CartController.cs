using Florive.Api.Attributes;
using Florive.BusinessLogic.Interface;
using Florive.DataAccess;
using Florive.Domains.Models;
using Microsoft.AspNetCore.Mvc;

namespace Florive.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private ICart _cartService;

        public CartController(AppDbContext context)
        {
            var bl = new Florive.BusinessLogic.BusinessLogic(context);
            _cartService = bl.GetCartActions();
        }

        [RequireAuth]
        [HttpGet("user/{userId}")]
        public IActionResult GetByUser(int userId)
        {
            var result = _cartService.GetCartByUserAction(userId);
            return Ok(result);
        }

        [RequireAuth]
        [HttpPost]
        public IActionResult AddToCart([FromBody] CartItemDTO dto)
        {
            var result = _cartService.AddToCartAction(dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [RequireAuth]
        [HttpPut("{id}")]
        public IActionResult UpdateCartItem(int id, [FromBody] CartItemDTO dto)
        {
            var result = _cartService.UpdateCartItemAction(id, dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [RequireAuth]
        [HttpDelete("{id}")]
        public IActionResult DeleteCartItem(int id)
        {
            var result = _cartService.DeleteCartItemAction(id);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        [RequireAuth]
        [HttpDelete("clear/{userId}")]
        public IActionResult ClearCart(int userId)
        {
            var result = _cartService.ClearCartAction(userId);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}