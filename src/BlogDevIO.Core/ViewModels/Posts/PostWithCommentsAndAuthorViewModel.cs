
using Blog_DevIO.Core.Entities;

namespace Blog_DevIO.Core.ViewModels.Post
{
    public class PostWithCommentsAndAuthorViewModel : PostViewModel
    {
        public Author Author { get; set; }
        public ICollection<Comment> Comments { get; set; }

        //COmments

        //Author

        public static PostWithCommentsAndAuthorViewModel New(Guid id, string title, string content, DateTime creation, bool canEdit, Author author, ICollection<Comment> comments)
        {
            return new PostWithCommentsAndAuthorViewModel
            {
                Id = id.ToString(),
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
