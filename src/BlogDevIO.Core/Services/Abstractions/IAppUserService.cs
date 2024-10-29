namespace Blog_DevIO.Core.Services.Abstractions
{
    public interface IAppUserService
    {
        public bool IsAuthenticated();
        public string? GetId();
        public bool IsAdmin();
    }
}
