using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Blog_DevIO.API.ViewModel
{
    public class CreateCommentViewModel
    {
        [Required(ErrorMessage = "The Content is Required")]
        [DisplayName("Content")]
        [DataType(DataType.MultilineText)]
        [MinLength(10)]
        public string Content { get; set; }
    }
}
