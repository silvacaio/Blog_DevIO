using Microsoft.AspNetCore.Identity;

namespace Blog_DevIO.Domain.Entities
{
    public class Comment : EntityBase
    {
        public Comment(string content)
            : base()
        {
            Content = content;
        }

        // Empty constructor for EF
        protected Comment() { }

        public string Content { get; set; }

        #region EF
        public Guid PostId { get; set; }
        public Post Post { get; set; }

        public string UserId { get; set; }

        public User User { get; private set; }

        #endregion
    }
}

