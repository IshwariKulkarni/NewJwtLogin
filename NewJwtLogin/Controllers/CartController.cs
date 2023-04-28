using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewJwtLogin.Dto;
using NewJwtLogin.Repos;


namespace NewJwtLogin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AddtoCartController : ControllerBase
    {
        private readonly ICartRepo _cartRepository;

        public AddtoCartController(ICartRepo cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpPost("name")]
        //[Authorize]
        public async Task<IActionResult> addtoCart([FromBody] CartDto cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _cartRepository.addtoCart(cart);

            if (result.IsSuccessful)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        //[HttpDelete("{id}")]
        ////[Authorize]
        //public async Task<ActionResult> RemoveCart(int id)
        //{
        //    await _cartRepository.RemoveCart(id);
        //    return NoContent();
        //}
    }
}

