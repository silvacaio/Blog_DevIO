namespace Blog_DevIO.Core.Entities
{
    public class Comment : EntityBase
    {
        public Comment(Guid id, string content, Guid postId, Guid authorId)
           : base(id)
        {
            Content = content;
            PostId = postId;
            AuthorId = authorId;
        }

        public Comment(string content, Guid postId, Guid authorId)
            : base()
        {
            Content = content;
            PostId = postId;
            AuthorId = authorId;
        }

        // Empty constructor for EF
        protected Comment() { }

        public string Content { get; private set; }

        #region EF
        public Guid PostId { get; set; }
        public Post Post { get; set; }

        public Guid AuthorId { get; set; }

        public Author Author { get; set; }

        #endregion
    }
}

