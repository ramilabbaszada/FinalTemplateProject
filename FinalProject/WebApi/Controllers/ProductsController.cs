using Business.Abstract;
using Core.Utilities.IoC;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {

            var result = await _productService.GetAll();

            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _productService.GetProductById(id);

            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Post(Product product)
        {
            var result = await _productService.Add(product);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Put(Product product)
        {
            var result = await _productService.Update(product);

            return Ok(result);
        }

    }
}
