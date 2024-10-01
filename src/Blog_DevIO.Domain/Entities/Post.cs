namespace Blog_DevIO.Domain.Entities
{
    public class Post : EntityBase
    {
        public Post(string title, string content,  DateTime creation)
            : base(creation)
        {
            Title = title;
            Content = content;        }

        public string Title { get; private set; }
        public string Content { get; private set; }
        public string[]? Tags { get; private set; }
      
        public ICollection<Comment> Comments { get; }
        public Guid UserId { get; set; }
        public User User { get; set; }

    }
}
