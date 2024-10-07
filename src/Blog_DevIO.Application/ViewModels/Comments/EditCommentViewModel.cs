using System.ComponentModel.DataAnnotations;

namespace Blog_DevIO.Application.ViewModels.Comments
{
    public class EditCommentViewModel : CreateCommentViewModel
    {
        [Required]
        public Guid Id { get; set; }
    }
}
