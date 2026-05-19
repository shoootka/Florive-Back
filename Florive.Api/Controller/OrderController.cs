using Florive.Api.Attributes;
using Florive.BusinessLogic.Interface;
using Florive.DataAccess;
using Florive.Domains.Models;
using Microsoft.AspNetCore.Mvc;

namespace Florive.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly Florive.BusinessLogic.Interface.IOrder _orderService;

        public OrderController(AppDbContext context)
        {
            var bl = new Florive.BusinessLogic.BusinessLogic(context);
            _orderService = bl.GetOrderActions();
        }

        [RequireAuth]
        [AdminMod]
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _orderService.GetAllOrdersAction();
            return Ok(result);
        }

        [RequireAuth]
        [HttpGet("user/{userId}")]
        public IActionResult GetByUser(int userId)
        {
            var result = _orderService.GetOrdersByUserAction(userId);
            return Ok(result);
        }

        [RequireAuth]
        [HttpPost]
        public IActionResult Create([FromBody] OrderDTO dto)
        {
            var result = _orderService.CreateOrderAction(dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [RequireAuth]
        [AdminMod]
        [HttpPut("status/{id}")]
        public IActionResult UpdateStatus(int id, [FromBody] string status)
        {
            var result = _orderService.UpdateOrderStatusAction(id, status);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [RequireAuth]
        [AdminMod]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _orderService.DeleteOrderAction(id);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }
    }
}