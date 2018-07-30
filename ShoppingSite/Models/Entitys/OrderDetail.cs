using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingSite.Models.Entitys
{
    public class OrderDetail
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual OrderHeader OrderHeader { get; set; }

        [Required]
        public int ProductsId { get; set; }

        [ForeignKey("ProductsId")]
        public virtual Products Products { get; set; }

        [Required]
        public int Count { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

    }
}