using Blog_DevIO.Domain.Entities;

namespace Blog_DevIO.Application.Services.Abstractions
{
    public interface IPostService
    {
        public Task<IEnumerable<Post?>> Get();

        public Task<Post?> GetById(Guid id);

        public Task<Post?> Delete(Guid id);
    }
}
