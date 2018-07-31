using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingSite.Models.Entitys
{
    public class Products
    {
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string Image { get; set; }

        [Required]
        public double Price { get; set; }

        [Required,Display(Name = "Strap Quality")]
        public string StrapQuality { get; set; }

        [Required, Display(Name = "Watch Brand")]
        public string Brand { get; set; }

        [Required, Display(Name = "Display Type")]
        public string DisplayType { get; set; }

        public bool IsAvailableInStock { get; set; }

        [Required, Range(0, 250)]
        [Display(Name = "Number in Stock")]
        public byte NumberAvailable { get; set; }

        public DateTime? CreatedTime { get; set; }

        public CategoryType CategoryType { get; set; }

        [Required]
        public int CategoryTypeId { get; set; }
    }
}