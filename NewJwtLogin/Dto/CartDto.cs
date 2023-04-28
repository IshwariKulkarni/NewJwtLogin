using System.ComponentModel.DataAnnotations;

namespace NewJwtLogin.Dto
{
    public class CartDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Product_Name { get; set; }

        [Required]
        public int Quantity { get; set; }
        public string Username { get; set; }
    }
}
