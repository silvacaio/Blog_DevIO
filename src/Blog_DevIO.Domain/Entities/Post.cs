using Microsoft.AspNetCore.Identity;

namespace Blog_DevIO.Domain.Entities
{
    public class Post : EntityBase
    {
        public Post(string title, string content)
            : base()
        {
            Title = title;
            Content = content;
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
