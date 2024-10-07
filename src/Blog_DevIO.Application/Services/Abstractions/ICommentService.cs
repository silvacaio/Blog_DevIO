using Blog_DevIO.Application.ViewModels.Comments;
using Blog_DevIO.Domain.Entities;

namespace Blog_DevIO.Application.Services.Abstractions
{
    public interface ICommentService
    {
        public Task<IEnumerable<Comment?>> Get();

        public Task<Comment?> GetById(Guid id);

        public Task<Comment?> Delete(Guid id);
        Task Create(CreateCommentViewModel comment, string userId);
        Task<Comment?> Update(EditCommentViewModel comment, string userId);
    }
}
