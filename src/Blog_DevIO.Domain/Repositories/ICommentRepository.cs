using Blog_DevIO.Domain.Entities;

namespace Blog_DevIO.Domain.Repositories
{
    public interface ICommentRepository : IRepositoryBase<Comment>
    {
        Task<IEnumerable<Comment?>> GetByPostId(Guid postId);
    }
}
