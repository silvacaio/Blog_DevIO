using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blog_DevIO.Application.ViewModels.Users
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "The Email is Required")]
        [EmailAddress]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Password is Required")]
        [MinLength(5)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
