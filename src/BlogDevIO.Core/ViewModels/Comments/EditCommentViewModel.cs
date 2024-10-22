using System.ComponentModel.DataAnnotations;

namespace Blog_DevIO.Core.ViewModels.Comments
{
    public class EditCommentViewModel : CreateCommentViewModel
    {
        [Required]
        public Guid Id { get; set; }
    }
}
