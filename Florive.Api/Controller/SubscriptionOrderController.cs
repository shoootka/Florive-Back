using Florive.Api.Attributes;
using Florive.BusinessLogic.Interface;
using Florive.DataAccess;
using Florive.Domains.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Florive.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionOrderController : ControllerBase
    {
        private ISubscriptionOrder _subscriptionOrderService;
        private AppDbContext _dbContext;

        public SubscriptionOrderController(AppDbContext context)
        {
            var bl = new Florive.BusinessLogic.BusinessLogic(context);
            _subscriptionOrderService = bl.GetSubscriptionOrderActions();
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _subscriptionOrderService.GetAllOrdersAction();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _subscriptionOrderService.GetOrderByIdAction(id);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] SubscriptionOrderDTO order)
        {
            var result = _subscriptionOrderService.CreateOrderAction(order);

            if (!result.IsSuccess)
                return BadRequest(result);

            return CreatedAtAction(nameof(GetById), new { id = ((Florive.Domains.Entities.SubscriptionOrder)result.Data).Id }, result);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] SubscriptionOrderDTO order)
        {
            var sessionKey = HttpContext.Request.Cookies["X-KEY"];
            var session = _dbContext.UserSessions
                .FirstOrDefault(s => s.SessionKey == sessionKey);

            var user = session != null
                ? _dbContext.Users.FirstOrDefault(u => u.Id == session.UserId)
                : null;

            var existingOrder = _dbContext.SubscriptionOrders
                .FirstOrDefault(o => o.Id == id);

            if (existingOrder == null)
                return NotFound(new { IsSuccess = false, Message = "Заказ не найден" });

            if (existingOrder.UserId != user?.Id)
                return Forbid();

            var result = _subscriptionOrderService.UpdateOrderAction(id, order);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _subscriptionOrderService.DeleteOrderAction(id);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }
    }
}