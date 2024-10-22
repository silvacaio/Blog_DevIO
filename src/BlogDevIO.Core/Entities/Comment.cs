namespace Blog_DevIO.Core.Entities
{
    public class Comment : EntityBase
    {
        public Comment(Guid id, string content, string postId, string userId)
           : base(id)
        {
            Content = content;
        }

        public Comment(string content, string postId, string userId)
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

