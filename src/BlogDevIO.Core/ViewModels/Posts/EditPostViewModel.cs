using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blog_DevIO.Core.ViewModels.Post
{
    public class EditPostViewModel : CreatePostViewModel
    {
        [Required]
        public Guid Id { get; set; }
    }
}
