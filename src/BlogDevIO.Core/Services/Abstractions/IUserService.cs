namespace Blog_DevIO.Core.Services.Abstractions
{
    public interface IUserService
    {
        public bool IsAuthenticated();
        public string? GetId();
        public bool IsAdmin();
    }
}
