using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewJwtLogin.Authentication;
using NewJwtLogin.Models;
using NewJwtLogin.Repos;
using System.Data;

namespace NewJwtLogin.Controllers
{
    [Authorize(Roles = UserRoles.User)]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartRepository _cartRepository;

        public CartController()
        {
            _cartRepository = new CartRepository();
        }

        [HttpPost("{cartId}/items")]
        public IActionResult AddToCart(int cartId, [FromBody] CartItem item)
        {
            _cartRepository.AddToCart(cartId, item);
            return Ok();
        }

        [HttpDelete("{cartId}/items/{itemId}")]
        public IActionResult RemoveFromCart(int cartId, int itemId)
        {
            _cartRepository.RemoveFromCart(cartId, itemId);
            return Ok();
        }

        [HttpGet("{cartId}")]
        public IActionResult GetCart(int cartId)
        {
            var cart = _cartRepository.GetCart(cartId);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }
    }
}
