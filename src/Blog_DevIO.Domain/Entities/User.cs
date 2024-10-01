namespace Blog_DevIO.Domain.Entities
{
    public class User : EntityBase
    {
        public User(string name, string lastName, string nickName, int age, DateTime creation)
            : base(creation)
        {
            Name = name;
            LastName = lastName;
            NickName = nickName;
            Age = age;
        }

        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string NickName { get; private set; }
        public int Age { get; private set; }
        public ICollection<Post> Posts { get; }

        public ICollection<Comment> Comments { get; }
    }
}
