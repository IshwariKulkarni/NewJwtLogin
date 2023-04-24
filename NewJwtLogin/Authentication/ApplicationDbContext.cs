using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewJwtLogin.Models;

namespace NewJwtLogin.Authentication
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> products { get; set; }

        public DbSet<Category> categories { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartItem> CartItems { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>().HasData(
  new Category { CategoryId = 1, CategoryName = "Clothing" },
  new Category { CategoryId = 2, CategoryName = "Electronic" },
  new Category { CategoryId = 3, CategoryName = "Cosmetics" }
  );
            base.OnModelCreating(builder);
        }
    }
}
