using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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