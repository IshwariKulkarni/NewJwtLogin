using System.ComponentModel.DataAnnotations;

namespace NewJwtLogin.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Required")]
        public string CategoryName { get; set; }
    }
}
