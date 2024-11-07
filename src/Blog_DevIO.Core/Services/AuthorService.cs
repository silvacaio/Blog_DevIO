using Blog_DevIO.Core.Data.Abstractions;
using Blog_DevIO.Core.Entities;
using Blog_DevIO.Core.Services.Abstractions;
using Blog_DevIO.Core.ViewModels.Authors;
using Blog_DevIO.Core.ViewModels.Users;
using Microsoft.AspNetCore.Identity;

namespace Blog_DevIO.Core.Services
{
    public class AuthorService : IAuthorService
    {
        private IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task Create(CreateAuthorViewModel model)
        {
            var author = new Author(model.Id, model.FistName, model.LastName);
            await _authorRepository.Save(author);
        }
    }
}
