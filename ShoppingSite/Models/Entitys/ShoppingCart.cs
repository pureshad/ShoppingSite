using System.ComponentModel.DataAnnotations;

namespace ShoppingSite.Models.Entitys
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int ProductsId { get; set; }

        [Required]
        public Products Products { get; set; }

        [Required]
        public User User { get; set; }

        public int UserId { get; set; }
    }
}