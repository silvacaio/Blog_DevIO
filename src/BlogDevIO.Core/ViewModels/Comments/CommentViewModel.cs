using Blog_DevIO.Core.Entities;
using Blog_DevIO.Core.ViewModels.Authors;

namespace Blog_DevIO.Core.ViewModels.Comments
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime Creation { get; set; }
        public Guid PostId { get; set; }
        public AuthorViewModel Author { get; set; }
        public bool CanEdit { get; set; } = false;
        public static CommentViewModel Load(Guid id, string content, DateTime creation, Guid postId, Author author, bool canEdit)
        {
            return new CommentViewModel
            {
                Id = id,
                Content = content,
                Creation = creation,
                Author = author != null ? AuthorViewModel.Load(author) : new AuthorViewModel(),
                CanEdit = canEdit,
                PostId = postId
            };
        }
    }
}
