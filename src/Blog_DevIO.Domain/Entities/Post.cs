using Microsoft.AspNetCore.Identity;

namespace Blog_DevIO.Domain.Entities
{
    public class Post : EntityBase
    {
        public Post(Guid id, string title, string content, string userId)
            : base(id)
        {
            Title = title;
            Content = content;
            UserId = userId;
        }

        public Post(string title, string content, string userId)
            : base()
        {
            Title = title;
            Content = content;
            UserId = userId;
        }

        // Empty constructor for EF
        protected Post() { }

        public string Title { get; private set; }
        public string Content { get; private set; }
        public string[]? Tags { get; private set; }

        #region EF
        public ICollection<Comment> Comments { get; }
        public string UserId { get; private set; }
        public User User { get; private set; }
        #endregion
    }
}
