using NewJwtLogin.Authentication;
using NewJwtLogin.Dto;
using NewJwtLogin.Models;
using static NewJwtLogin.Repos.CartRepo;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace NewJwtLogin.Repos
{
    public interface ICartRepo
    {
        Task<CartAdd> addtoCart(CartDto cart);
        //Task RemoveCart(int id);
    }


    public class CartRepo : ICartRepo
    {
        private readonly ApplicationDbContext _context;

        public CartRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CartAdd> addtoCart(CartDto cart)
        {
            var products = await _context.products.FirstOrDefaultAsync(f => f.ProductName == cart.Product_Name);

            if (products == null)
            {
                return new CartAdd(false, "Invalid Product Name");
            }

            // Calculate total price based on the number of tickets and flight price
            decimal totalPrice = (decimal)(products.ProductPrice * cart.Quantity);



            // Create a new Booking entity

            var newCart = new Cart
            {
                Username = cart.Username,
                Product_Name = cart.Product_Name,
                Email = cart.Email,
                //Description = cart.Description,
                //Product_price = cart.Product_price,
                //Category = cart.Category,
                Quantity = cart.Quantity,

                TotalPrice = totalPrice,
                //Product_Id = product.Id
            };

            // Add the new booking to the database
            _context.carts.Add(newCart);
            await _context.SaveChangesAsync();
            // Send confirmation email to user
            await SendConfirmationEmailAsync(products, cart, totalPrice, "youremail@example.com");

            return new CartAdd(true, "");
        }


        private async Task SendConfirmationEmailAsync(Product product, CartDto cart, decimal totalPrice, string fromEmail)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(cart.Email));
            message.Subject = "Product Delivery Confirmation";
            message.Body = $"Dear {cart.Username},\n\nThank you for Shopping with us:\n\nProduct Name: {cart.Product_Name}\nProduct Quantity: {cart.Quantity}\nTotal Price: {totalPrice}\n\nYou can contact us if you have any querries and we would be happy to help you.\n\nSincerely,\nThe ShoppingCart Team";

            message.From = new MailAddress(fromEmail);

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "nishikawaghmare08@gmail.com",
                    Password = "fhpvoqdqckoqewio"
                };
                smtp.UseDefaultCredentials = false;

                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;


                await smtp.SendMailAsync(message);
            }
        }

        public class CartAdd
        {
            public bool IsSuccessful { get; }
            public string ErrorMessage { get; }

            public CartAdd(bool isSuccessful, string errorMessage)
            {
                IsSuccessful = isSuccessful;
                ErrorMessage = errorMessage;
            }
        }

        //public async Task RemoveCart(int id)
        //{
        //    var booking = await _context.Carts.FindAsync(id);

        //    if (booking == null)
        //    {
        //        throw new ArgumentException("Booking not found");
        //    }

        //    _context.Carts.Remove(booking);
        //    await _context.SaveChangesAsync();
        //}
    }
}
