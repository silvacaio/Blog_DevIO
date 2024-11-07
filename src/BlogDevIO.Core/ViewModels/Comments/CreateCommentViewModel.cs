using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Blog_DevIO.Core.ViewModels.Comments
{
    public class CreateCommentViewModel
    {
        [Required(ErrorMessage = "The Content is Required")]
        [DisplayName("Content")]
        [DataType(DataType.MultilineText)]
        [MinLength(10)]
        public string Content { get; set; }

        [Required(ErrorMessage = "The PostId is Required")]
        [DisplayName("Post")]
        public Guid PostId { get; set; }

        public static CreateCommentViewModel Create(Guid postId, string content)
        {
            return new CreateCommentViewModel
            {
                PostId = postId,
                Content = content
            };
        }
    }
}
