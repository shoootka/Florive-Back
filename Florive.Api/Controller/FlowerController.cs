using Florive.Domains.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Florive.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowerController : ControllerBase
    {
        private static List<Flower> _flowers = new List<Flower>();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_flowers);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var flower = _flowers.FirstOrDefault(f => f.Id == id);

            if (flower == null)
            {
                return NotFound(new { Message = $"Flower with ID {id} not found" });
            }

            return Ok(flower);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Flower flower)
        {
            flower.Id = _flowers.Count + 1;

            _flowers.Add(flower);

            return Created($"/api/flower/{flower.Id}", flower);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Flower updatedFlower)
        {
            var existingFlower = _flowers.FirstOrDefault(f => f.Id == id);

            if (existingFlower == null)
            {
                return NotFound(new { Message = $"Flower with ID {id} not found" });
            }

            existingFlower.Name = updatedFlower.Name;
            existingFlower.Price = updatedFlower.Price;
            existingFlower.Category = updatedFlower.Category;
            existingFlower.Image = updatedFlower.Image;

            return Ok(existingFlower);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var flower = _flowers.FirstOrDefault(f => f.Id == id);

            if (flower == null)
            {
                return NotFound(new { Message = $"Flower with ID {id} not found" });
            }

            _flowers.Remove(flower);

            return NoContent();
        }

    }
}
    