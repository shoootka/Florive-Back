using Florive.Api.Attributes;
using Florive.BusinessLogic.Interface;
using Florive.DataAccess;
using Florive.Domains.Entities;
using Florive.Domains.Models;
using Microsoft.AspNetCore.Mvc;

namespace Florive.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionPlanController : ControllerBase
    {
        private ISubscriptionPlan _subscriptionPlanService;

        public SubscriptionPlanController(AppDbContext context)
        {
            var bl = new Florive.BusinessLogic.BusinessLogic(context);
            _subscriptionPlanService = bl.GetSubscriptionPlanActions();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _subscriptionPlanService.GetAllPlansAction();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _subscriptionPlanService.GetPlanByIdAction(id);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        [RequireAuth]
        [RequireRole("Admin")]
        [HttpPost]
        public IActionResult Create([FromBody] SubscriptionPlanDTO plan)
        {
            var result = _subscriptionPlanService.CreatePlanAction(plan);

            if (!result.IsSuccess)
                return BadRequest(result);

            return CreatedAtAction(nameof(GetById), new { id = ((Florive.Domains.Entities.SubscriptionPlan)result.Data).Id }, result);
        }

        [RequireAuth]
        [RequireRole("Admin")]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] SubscriptionPlanDTO plan)
        {
            var result = _subscriptionPlanService.UpdatePlanAction(id, plan);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [RequireAuth]
        [RequireRole("Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _subscriptionPlanService.DeletePlanAction(id);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }
    }
}