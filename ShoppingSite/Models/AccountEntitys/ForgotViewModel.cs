using System.ComponentModel.DataAnnotations;

namespace ShoppingSite.Models.AccountEntitys
{

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
