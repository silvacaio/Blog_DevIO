using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Blog_DevIO.Core.Services.Abstractions
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetId()
        {
            if (IsAuthenticated() == false)
                return string.Empty;

            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public bool IsAdmin()
        {
            if (IsAuthenticated() == false)
                return false;

            return _httpContextAccessor.HttpContext?.User?.IsInRole("SuperAdmin") ?? false;
        }

        public bool IsAuthenticated()
        {
            return _httpContextAccessor.HttpContext?.User.Identity is { IsAuthenticated: true };
        }

        public bool IsLoggedUser(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId) == true)
                return false;

            return userId == GetId();
        }

        public bool IsOwnerOrAdmin(string userId)
        {
            if (IsAdmin() == true)
                return true;

            return IsOwnerOrAdmin(userId);
        }
    }
}
