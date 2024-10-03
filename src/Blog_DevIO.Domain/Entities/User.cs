using Microsoft.AspNetCore.Identity;

namespace Blog_DevIO.Domain.Entities
{
    public class User : IdentityUser
    {
        public ICollection<Post> Posts { get; }

        public ICollection<Comment> Comments { get; }
    }
}
