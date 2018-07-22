using ShoppingSite.Models.Entitys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoppingSite.ViewModels
{
    public class ProductsViewModel
    {
        public ProductsViewModel()
        {
            Id = 0;
        }

        public ProductsViewModel(Products products)
        {
            Id = products.Id;
            Name = products.Name;
            CreatedTime = products.CreatedTime;
            NumberAvailable = products.NumberAvailable;
            Brand = products.Brand;
            Description = products.Description;
            CategoryTypeId = products.CategoryTypeId;
            Image = products.Image;
        }

        public IEnumerable<CategoryType> CategoryTypes { get; set; }

        public int? Id { get; set; }

        [Required, Display(Name = "Category")]
        public int? CategoryTypeId { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string Image { get; set; }

        [Required]
        public double? Price { get; set; }

        [Required, Display(Name = "Strap Quality")]
        public string StrapQuality { get; set; }

        [Required, Display(Name = "Watch Brand")]
        public string Brand { get; set; }

        [Required, Display(Name = "Display Type")]
        public string DisplayType { get; set; }

        [Required, Range(1, 250)]
        [Display(Name = "Number in Stock")]
        public byte? NumberAvailable { get; set; }

        public DateTime? CreatedTime { get; set; }
    }
}