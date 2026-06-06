using Florive.BusinessLogic.Interface;
using Florive.DataAccess;
using Florive.Domains.Models;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _orderService.GetAllOrdersAction();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("user/{userId}")]
        public IActionResult GetByUser(int userId)
        {
            var result = _orderService.GetOrdersByUserAction(userId);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] OrderDTO dto)
        {
            var result = _orderService.CreateOrderAction(dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("status/{id}")]
        public IActionResult UpdateStatus(int id, [FromBody] string status)
        {
            var result = _orderService.UpdateOrderStatusAction(id, status);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _orderService.DeleteOrderAction(id);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        [Authorize]
        [HttpPut("cancel/{id}")]
        public IActionResult CancelOrder(int id)
        {
            var result = _orderService.UpdateOrderStatusAction(id, "Cancelled");

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}