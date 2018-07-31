using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingSite.Models.Entitys
{
    public class OrderHeader
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public DateTime? OrderDate { get; set; }

        [Required]
        public double OrderTotal { get; set; }

        public string Comments { get; set; }

        public string Status { get; set; }
    }
}