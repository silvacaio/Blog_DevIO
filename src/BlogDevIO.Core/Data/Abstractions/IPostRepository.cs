using Blog_DevIO.Core.Entities;

namespace Blog_DevIO.Core.Data.Abstractions
{
    public interface IPostRepository : IRepositoryBase<Post>
    {
        Task<IEnumerable<Post?>> GetByUser(string userId);
    }
}
