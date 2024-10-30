using Blog_DevIO.Core.ViewModels.Users;
using Microsoft.AspNetCore.Identity;

namespace Blog_DevIO.Core.Services.Abstractions
{
    public interface IUserService
    {
        public Task<IdentityResult> Create(RegisterViewModel model);
    }
}
