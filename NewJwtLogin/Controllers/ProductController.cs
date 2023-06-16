using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewJwtLogin.Dto;
using NewJwtLogin.Models;
using NewJwtLogin.Repos;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewJwtLogin.Controllers
{
    //[Authorize(Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("GetAllProducts")]
        public async Task<List<Product>> GetAll()
        {
            return await _productRepository.GetAllAsync();
        }

        [HttpGet("GetProductById/{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<Product> GetById(int id)
        {
            return await _productRepository.GetProductById(id);
        }

        [HttpPost("AddProduct")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Create(ProductDto product)
        {
            await _productRepository.Create(product);
            return Ok();
        }

        [HttpPut("UpdateProduct/{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> UpdateProduct(int id, ProductDto product)
        {
            /*            if (id != product.ProductId)
                        {
                            return BadRequest();
                        }*/

            await _productRepository.UpdateProduct(id, product);
            return Ok();
        }

        [HttpDelete("DeleteProduct/{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            await _productRepository.DeleteAsync(id);
            return Ok();
        }

        //[AllowAnonymous]
        [HttpGet("search/{ProductName}")]
        public async Task<ActionResult<IEnumerable<Product>>> Search(string ProductName)
        {
            try
            {
                var result = await _productRepository.Search(ProductName);
                if (result.Any())
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error in Retrieving Data from Database");

            }

        }
    }
}
