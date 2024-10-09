using Blog_DevIO.Application.ViewModels.Comments;
using Blog_DevIO.Domain.Entities;

namespace Blog_DevIO.Application.Services.Abstractions
{
    public interface ICommentService
    {
        public Task<IEnumerable<Comment?>> Get();

        public Task<Comment?> GetById(Guid id);

        public Task Delete(Guid id);
        Task Create(CreateCommentViewModel comment);
        Task<Comment?> Update(EditCommentViewModel comment);

        Task<Comment?> GetCommentToAction(Guid commentId);
    }
}
