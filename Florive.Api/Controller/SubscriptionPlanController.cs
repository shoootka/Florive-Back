using Microsoft.AspNetCore.Mvc;
using Florive.Api.Domain;

namespace Florive.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionPlanController : ControllerBase
    {
        private static List<SubscriptionPlan> _subscriptionPlans = new List<SubscriptionPlan>();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_subscriptionPlans);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var subscriptionPlan = _subscriptionPlans.FirstOrDefault(s => s.Id == id);

            if (subscriptionPlan == null)
            {
                return NotFound(new { Message = $"Subscription plan with id {id} not found" });
            }

            return Ok(subscriptionPlan);
        }

        [HttpPost]
        public IActionResult Create([FromBody] SubscriptionPlan subscriptionPlan)
        {
            subscriptionPlan.Id = _subscriptionPlans.Count + 1;

            _subscriptionPlans.Add(subscriptionPlan);

            return Created($"/api/subscriptionplan/{subscriptionPlan.Id}", subscriptionPlan);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] SubscriptionPlan updatedSubscriptionPlan)
        {
            var existingSubscriptionPlan = _subscriptionPlans.FirstOrDefault(s => s.Id == id);

            if (existingSubscriptionPlan == null)
            {
                return NotFound(new { Message = $"Subscription plan with id {id} not found" });
            }

            existingSubscriptionPlan.Name = updatedSubscriptionPlan.Name;
            existingSubscriptionPlan.Price = updatedSubscriptionPlan.Price;
            existingSubscriptionPlan.DeliveriesCount = updatedSubscriptionPlan.DeliveriesCount;
            existingSubscriptionPlan.Description = updatedSubscriptionPlan.Description;

            return Ok(existingSubscriptionPlan);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var subscriptionPlan = _subscriptionPlans.FirstOrDefault(s => s.Id == id);

            if (subscriptionPlan == null)
            {
                return NotFound(new { Message = $"Subscription plan with id {id} not found" });
            }

            _subscriptionPlans.Remove(subscriptionPlan);

            return NoContent();
        }
    }
}