using System.ComponentModel.DataAnnotations;

namespace ShoppingSite.Models.Entitys
{
    public class CategoryType
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}