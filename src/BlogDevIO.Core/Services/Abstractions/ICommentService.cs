using Blog_DevIO.Core.ViewModels.Comments;
using Blog_DevIO.Core.Entities;

namespace Blog_DevIO.Core.Services.Abstractions
{
    public interface ICommentService
    {
        public Task<IEnumerable<CommentViewModel?>> Get();

        public Task<CommentViewModel?> GetById(Guid id);

        public Task Delete(Guid id);
        Task Create(CreateCommentViewModel comment);
        Task<Comment?> Update(EditCommentViewModel comment);

        Task<Comment?> GetCommentToAction(Guid commentId);
        Task<IEnumerable<CommentViewModel?>> GetByPostId(Guid postId);
        CommentViewModel CreateCommentViewModel(Comment comment);
    }
}
