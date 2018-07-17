using System.ComponentModel.DataAnnotations;

namespace ShoppingSite.Models.AccountEntitys
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
