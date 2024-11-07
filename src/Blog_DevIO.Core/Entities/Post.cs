using Microsoft.AspNetCore.Identity;

namespace Blog_DevIO.Core.Entities
{
    public class Post : EntityBase
    {
        public Post(Guid id, string title, string content, Guid authorId)
            : base(id)
        {
            Title = title;
            Content = content;
            AuthorId = authorId;
        }

        public Post(string title, string content, Guid authorId)
            : base()
        {
            Title = title;
            Content = content;
            AuthorId = authorId;
        }

        // Empty constructor for EF
        protected Post() { }

        public string Title { get; private set; }
        public string Content { get; private set; }

        #region EF
        public ICollection<Comment> Comments { get; }
        public Guid AuthorId { get; set; }
        public Author Author { get; set; }
        #endregion
    }
}
