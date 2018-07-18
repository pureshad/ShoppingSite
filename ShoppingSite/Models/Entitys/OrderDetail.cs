using System.ComponentModel.DataAnnotations;

namespace ShoppingSite.Models.Entitys
{
    public class OrderDetail
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        public virtual OrderHeader OrderHeader { get; set; }

        [Required]
        public int MenuItemId { get; set; }

        public virtual Products Products { get; set; }

        [Required]
        public int Count { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

    }
}