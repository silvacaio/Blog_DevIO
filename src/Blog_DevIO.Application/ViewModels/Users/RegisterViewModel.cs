using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blog_DevIO.Application.ViewModels.Users
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "The Email is Required")]
        [EmailAddress]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Password is Required")]
        [MinLength(5)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "The FirstName is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        public string FistName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
    }
}
