using Florive.Api.Attributes;
using Florive.BusinessLogic.Interface;
using Florive.DataAccess;
using Florive.Domains.Entities;
using Florive.Domains.Models;
using Microsoft.AspNetCore.Authorization;
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
            var bl = new Florive.BusinessLogic.BusinessLogic(context);
            _productService = bl.GetProductActions();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            var result = _productService.GetAllProductsAction();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetProductByIdAction(id);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create([FromBody] ProductDTO product)
        {
            var result = _productService.CreateProductAction(product);

            if (!result.IsSuccess)
                return BadRequest(result);

            return CreatedAtAction(nameof(GetById), new { id = ((Product)result.Data).Id }, result);
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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
    