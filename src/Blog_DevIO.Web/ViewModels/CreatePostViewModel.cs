using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Blog_DevIO.Web.ViewModels
{
    public class CreatePostViewModel
    {
        public CreatePostViewModel()
        {
        }

        [Required(ErrorMessage = "The Title is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The Content is Required")]
        [MinLength(2)]
        [DisplayName("Title")]
        public string Content { get; set; }
        public string[]? Tags { get; set; }
    }
}
