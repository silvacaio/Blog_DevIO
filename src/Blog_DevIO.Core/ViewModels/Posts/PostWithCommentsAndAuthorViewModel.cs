
using Blog_DevIO.Core.Entities;
using Blog_DevIO.Core.ViewModels.Authors;
using Blog_DevIO.Core.ViewModels.Comments;

namespace Blog_DevIO.Core.ViewModels.Post
{
    public class PostWithCommentsAndAuthorViewModel : PostViewModel
    {
        public AuthorViewModel Author { get; set; }
        public ICollection<CommentViewModel> Comments { get; set; }

        //COmments

        //Author

        public static PostWithCommentsAndAuthorViewModel New(Guid id, string title, string content, DateTime creation, bool canEdit, AuthorViewModel author, ICollection<CommentViewModel> comments)
        {
            return new PostWithCommentsAndAuthorViewModel
            {
                Id = id,
                Title = title,
                Content = content,
                CanEdit = canEdit,
                Creation = creation,
                Author = author,
                Comments = comments,
            };
        }
    }
}
