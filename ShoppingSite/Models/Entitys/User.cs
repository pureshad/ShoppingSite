using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingSite.Models.Entitys
{
    public class User
    {
        public int Id { get; set; }

        [Required, StringLength(80)]
        public string FirstName { get; set; }

        [Required, StringLength(80)]
        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        [Required]
        public string Email { get; set; }
    }
}