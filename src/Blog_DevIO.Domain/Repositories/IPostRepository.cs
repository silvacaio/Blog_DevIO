using Blog_DevIO.Domain.Entities;

namespace Blog_DevIO.Domain.Repositories
{
    public interface IPostRepository : IRepositoryBase<Post>
    {
        Task<IEnumerable<Post?>> GetByUser(string userId);
    }
}
