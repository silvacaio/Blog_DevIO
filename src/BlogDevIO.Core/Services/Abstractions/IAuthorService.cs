using Blog_DevIO.Core.ViewModels.Authors;

namespace Blog_DevIO.Core.Services.Abstractions
{
    public interface IAuthorService
    {
        Task Create(CreateAuthorViewModel model);
    }
}
