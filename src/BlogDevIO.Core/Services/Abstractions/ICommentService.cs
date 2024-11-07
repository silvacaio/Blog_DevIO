using Blog_DevIO.Core.ViewModels.Comments;
using Blog_DevIO.Core.Entities;

namespace Blog_DevIO.Core.Services.Abstractions
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentViewModel?>> Get();

        Task<CommentViewModel?> GetById(Guid id);
        Task<CommentViewModel?> GetByPostIdAndId(Guid postId, Guid id);

        Task Create(CreateCommentViewModel comment);
        Task<Comment?> GetCommentToAction(Guid postId, Guid id);
        Task<IEnumerable<CommentViewModel?>> GetByPostId(Guid postId);
        CommentViewModel CreateCommentViewModel(Comment comment);
        Task Delete(Guid postId, Guid id);
        Task Update(CommentViewModel comment);
    }
}
