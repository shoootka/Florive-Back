using Florive.BusinessLogic.Interface;
using Florive.DataAccess;
using Florive.Domains.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Florive.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubscriptionOrderController : ControllerBase
    {
        private readonly ISubscriptionOrder _subscriptionOrderService;
        private readonly AppDbContext _dbContext;

        public SubscriptionOrderController(AppDbContext context)
        {
            _dbContext = context;

            var bl = new Florive.BusinessLogic.BusinessLogic(context);
            _subscriptionOrderService = bl.GetSubscriptionOrderActions();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _subscriptionOrderService.GetAllOrdersAction();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _subscriptionOrderService.GetOrderByIdAction(id);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] SubscriptionOrderDTO order)
        {
            var result = _subscriptionOrderService.CreateOrderAction(order);

            if (!result.IsSuccess)
                return BadRequest(result);

            return CreatedAtAction(
                nameof(GetById),
                new { id = ((Florive.Domains.Entities.SubscriptionOrder)result.Data).Id },
                result
            );
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] SubscriptionOrderDTO order)
        {
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