using NewJwtLogin.Models;

namespace NewJwtLogin.Repos
{
    public class CartRepository
    {
        private readonly List<Cart> _carts;

        public CartRepository()
        {
            _carts = new List<Cart>();
        }

        public void AddToCart(int cartId, CartItem item)
        {
            var cart = _carts.FirstOrDefault(c => c.Id == cartId);
            if (cart == null)
            {
                cart = new Cart { Id = cartId };
                _carts.Add(cart);
            }
            cart.Items.Add(item);
        }

        public void RemoveFromCart(int cartId, int itemId)
        {
            var cart = _carts.FirstOrDefault(c => c.Id == cartId);
            if (cart == null) return;
            var itemToRemove = cart.Items.FirstOrDefault(i => i.Id == itemId);
            if (itemToRemove != null)
            {
                cart.Items.Remove(itemToRemove);
            }
        }

        public Cart GetCart(int cartId)
        {
            return _carts.FirstOrDefault(c => c.Id == cartId);
        }
    }
}
