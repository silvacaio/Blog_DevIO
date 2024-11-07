
namespace Blog_DevIO.Core.ViewModels.Authors
{
    public class CreateAuthorViewModel
    {
        public Guid Id { get; set; }
        public string FistName { get; set; }
        public string LastName { get; set; }

        public static CreateAuthorViewModel Create(Guid id, string firstName, string lastName)
        {
            return new CreateAuthorViewModel
            {
                Id = id,
                FistName = firstName,
                LastName = lastName
            };
        }
    }
}
