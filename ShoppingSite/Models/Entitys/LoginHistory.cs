using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingSite.Models.Entitys
{
    public class LoginHistory
    {
        public int Id { get; set; }

        public DateTime? LoginTime { get; set; }

        public DateTime? LogoutTime { get; set; }

        public bool IsLogedIn { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }
    }
}