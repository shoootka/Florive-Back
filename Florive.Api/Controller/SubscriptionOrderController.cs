using Microsoft.AspNetCore.Mvc;
using Florive.Api.Domain;

namespace Florive.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionOrderController : ControllerBase
    {
        private static List<SubscriptionOrder> _orders = new List<SubscriptionOrder>();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_orders);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return NotFound(new { Message = $"Order with id {id} not found" });
            }

            return Ok(order);
        }

        [HttpPost]
        public IActionResult Create([FromBody] SubscriptionOrder order)
        {
            order.Id = _orders.Count + 1;
            order.Status = "New";

            _orders.Add(order);

            return Created($"/api/subscriptionorder/{order.Id}", order);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] SubscriptionOrder updatedOrder)
        {
            var existingOrder = _orders.FirstOrDefault(o => o.Id == id);

            if (existingOrder == null)
            {
                return NotFound(new { Message = $"Order with id {id} not found" });
            }

            existingOrder.Name = updatedOrder.Name;
            existingOrder.Phone = updatedOrder.Phone;
            existingOrder.Email = updatedOrder.Email;
            existingOrder.Address = updatedOrder.Address;
            existingOrder.Frequency = updatedOrder.Frequency;
            existingOrder.FirstDeliveryDate = updatedOrder.FirstDeliveryDate;
            existingOrder.Comment = updatedOrder.Comment;
            existingOrder.Status = updatedOrder.Status;

            return Ok(existingOrder);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return NotFound(new { Message = $"Order with ID {id} not found" });
            }

            _orders.Remove(order);

            return NoContent();
        }
    }
}