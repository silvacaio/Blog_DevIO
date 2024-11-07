using System.ComponentModel.DataAnnotations;

namespace Blog_DevIO.Core.ViewModels.Comments
{
    public class EditCommentViewModel : CreateCommentViewModel
    {
        [Required]
        public Guid Id { get; set; }

        public static implicit operator CommentViewModel(EditCommentViewModel commentEdit)
        {
            return new CommentViewModel
            {
                Id = commentEdit.Id,
                Content = commentEdit.Content,
                PostId = commentEdit.PostId
            };
        }
    }
}
