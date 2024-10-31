using Blog_DevIO.Core.Entities;
using Blog_DevIO.Core.ViewModels.Authors;

namespace Blog_DevIO.Core.ViewModels.Comments
{
    public class CommentViewModel
    {
        public string Content { get; set; }
        public DateTime Creation { get; set; }
        public AuthorViewModel Author { get; set; }
        public bool CanEdit { get; set; } = false;
        public static CommentViewModel Load(Guid id, string content, DateTime creation, Author author, bool canEdit)
        {
            return new CommentViewModel
            {
                Content = content,
                Creation = creation,
                Author = AuthorViewModel.Load(author),
                CanEdit = canEdit
            };
        }
    }
}
