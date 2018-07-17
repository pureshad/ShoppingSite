using System.ComponentModel.DataAnnotations;

namespace ShoppingSite.Models.ManageEntitys
{

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }
}