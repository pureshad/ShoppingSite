using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingSite.Models.Entitys
{
    public class OrderHeader
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public double OrderTotal { get; set; }

        public string Comments { get; set; }
    }
}