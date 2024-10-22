using Microsoft.AspNetCore.Identity;

namespace Blog_DevIO.Core.Entities
{
    public class User : IdentityUser
    {
        public User(string fistName, string lastName)
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
