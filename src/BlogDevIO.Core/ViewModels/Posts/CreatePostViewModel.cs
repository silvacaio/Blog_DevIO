using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blog_DevIO.Core.ViewModels.Post
{
    public class CreatePostViewModel
    {
        [Required(ErrorMessage = "The Title is Required")]
        [MinLength(5)]
        [MaxLength(100)]
        [DisplayName("Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The Content is Required")]
        [DisplayName("Content")]
        [DataType(DataType.MultilineText)]
        [MinLength(10)]
        public string Content { get; set; }
    }
}
