using System.ComponentModel.DataAnnotations;

namespace Blog_DevIO.Core.ViewModels.Post
{
    public class EditPostViewModel : CreatePostViewModel
    {
        [Required]
        public Guid Id { get; set; }

        public static implicit operator PostViewModel(EditPostViewModel postEdit)
        {
            return new PostViewModel
            {
                Id = postEdit.Id,
                Content = postEdit.Content,
                Title = postEdit.Title
            };
        }
    }
}
