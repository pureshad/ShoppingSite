using Microsoft.AspNet.Identity.EntityFramework;
using ShoppingSite.Models.Entitys;
using System.Data.Entity;

namespace ShoppingSite.Models
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<CategoryType> CategoryTypes { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}