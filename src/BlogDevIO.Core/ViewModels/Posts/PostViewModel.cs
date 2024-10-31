
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Blog_DevIO.Core.ViewModels.Post
{
    public class PostViewModel
    {
        public Guid Id { get; set; }

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
        public DateTime Creation { get; set; }
        public bool CanEdit { get; set; } = false;

        public static PostViewModel New(Guid id, string title, string content, DateTime creation, bool canEdit)
        {
            return new PostViewModel
            {
                Id = id,
                Title = title,
                Content = content,
                CanEdit = canEdit,
                Creation = creation
            };
        }
    }
}
