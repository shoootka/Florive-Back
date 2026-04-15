using Florive.BusinessLogic.Interface;
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

        public ProductController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _productService = bl.GetProductActions();
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
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductDTO product)
        {
            var result = _productService.CreateProductAction(product);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProductDTO product)
        {
            var result = _productService.UpdateProductAction(id, product);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _productService.DeleteProductAction(id);
            return Ok(result);
        }

    }
}
    