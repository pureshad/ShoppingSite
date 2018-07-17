using System.ComponentModel.DataAnnotations;

namespace ShoppingSite.Models.AccountEntitys
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
