using System.Reflection.Metadata;

namespace Blog_DevIO.Domain.Entities
{
    public class Comment : EntityBase
    {
        public Comment(string content, DateTime creation)
            : base(creation)
        {
            Content = content;
            // Author = author;
        }

        public string Content { get; private set; }

        #region EF
        public Guid PostId { get; set; }
        public Post Post { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        #endregion
    }
}

