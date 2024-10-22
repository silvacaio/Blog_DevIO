
namespace Blog_DevIO.Domain.Entities
{
    public class Author : EntityBase
    {
        public string Id { get; set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Bio { get; private set; }
        public string UserId { get; private set; }
    }
}
