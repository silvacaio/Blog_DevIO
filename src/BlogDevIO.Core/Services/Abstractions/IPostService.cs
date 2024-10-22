using Blog_DevIO.Core.Entities;
using Blog_DevIO.Core.ViewModels.Post;

namespace Blog_DevIO.Core.Services.Abstractions
{
    public interface IPostService
    {
        public Task<IEnumerable<Post?>> Get();
        public Task<Post?> GetById(Guid id);
        Task<IEnumerable<Post?>> GetByUser();
        public Task Create(CreatePostViewModel post);
        Task<Post?> Update(EditPostViewModel comment);
        public Task Delete(Guid id);
        Task<Post?> GetPostToAction(Guid postId);
    }
}
