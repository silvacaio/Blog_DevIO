namespace Blog_DevIO.Core.Entities
{
    public class Author : EntityBase
    {
        public Author(Guid id, string fistName, string lastName)
          : base(id)
        {
            FistName = fistName;
            LastName = lastName;
        }

        public string FistName { get; private set; }
        public string LastName { get; private set; }

        public ICollection<Post> Posts { get; }

        public ICollection<Comment> Comments { get; }
    }
}
