using Blog_DevIO.Domain.Entities;
using Blog_DevIO.Application.ViewModels.Post;

namespace Blog_DevIO.Application.Services.Abstractions
{
    public interface IPostService
    {
        public Task<IEnumerable<Post?>> Get();
        public Task<Post?> GetById(Guid id);
        Task<IEnumerable<Post?>> GetByUser(string userId);
        public Task Create(CreatePostViewModel post, string userId);
        Task<Post?> Update(EditPostViewModel comment, string userId);
        public Task Delete(Guid id);
    }
}
