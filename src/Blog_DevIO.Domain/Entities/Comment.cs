
namespace Blog_DevIO.Domain.Entities
{
    public class Comment : EntityBase
    {
        public Comment(string content)
            : base()
        {
            Content = content;
        }

        public string Content { get; private set; }

        #region EF
        public Guid PostId { get; set; }
        public Post Post { get; set; }

        public Guid UserId { get; set; }

        #endregion
    }
}

