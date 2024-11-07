
namespace Blog_DevIO.Core.ViewModels.Authors
{
    public class AuthorViewModel
    {
        public Guid Id { get; set; }
        public string FistName { get; set; }
        public string LastName { get; set; }

        public static AuthorViewModel Load(Entities.Author author)
        {
            return new AuthorViewModel
            {
                FistName = author.FistName,
                LastName = author.LastName,
                Id = author.Id
            };
        }
    }
}
