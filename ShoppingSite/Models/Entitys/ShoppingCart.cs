using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingSite.Models.Entitys
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public int ProductsId { get; set; }

        [Required]
        public Products Products { get; set; }

        [Required]
        [NotMapped]
        public ApplicationUser ApplicationUser { get; set; }

        public int UserId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger the {1}")]
        public int Count { get; set; }
    }
}