namespace Blog_DevIO.Application.Services.Abstractions
{
    public interface IUserService
    {
        public bool IsAuthenticated();
        public string? GetId();
        public bool IsAdmin();
    }
}
