using Blog_DevIO.Core.Data.Abstractions;
using Blog_DevIO.Core.Entities;
using Blog_DevIO.Core.Services.Abstractions;
using Blog_DevIO.Core.ViewModels.Users;
using Microsoft.AspNetCore.Identity;

namespace Blog_DevIO.Core.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private IAuthorRepository _authorRepository;

        public UserService(UserManager<IdentityUser> userManager, IAuthorRepository authorRepository)
        {
            _userManager = userManager;
            _authorRepository = authorRepository;
        }

        public async Task<IdentityResult> Create(RegisterViewModel model)
        {
            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded == false)
                return result;

            var author = new Author(Guid.Parse(user.Id), model.FistName, model.LastName);

            await _authorRepository.Save(author);
            return result;
        }
    }
}
