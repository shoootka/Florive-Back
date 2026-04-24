using Florive.BusinessLogic.Interface;
using Florive.DataAccess;
using Florive.Domains.Entities;
using Florive.Domains.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Florive.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProduct _productService;

        public ProductController(AppDbContext context)
        {
            _productService = new Florive.BusinessLogic.Functions.Products.ProductFunction(context);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _productService.GetAllProductsAction();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetProductByIdAction(id);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductDTO product)
        {
            var result = _productService.CreateProductAction(product);

            if (!result.IsSuccess)
                return BadRequest(result);

            return CreatedAtAction(nameof(GetById), new { id = ((Product)result.Data).Id }, result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProductDTO product)
        {
            var result = _productService.UpdateProductAction(id, product);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _productService.DeleteProductAction(id);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

    }
}
    