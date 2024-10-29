using Blog_DevIO.Core.Entities;

namespace Blog_DevIO.Core.Data.Abstractions
{
    public interface IPostRepository : IRepositoryBase<Post>
    {
        Task<IEnumerable<Post?>> GetByUser(Guid userId);
        Task<Post?> GetById(Guid id, bool includeComments, bool includeAuthor);
        Task<IEnumerable<Post?>> GetAll(bool includeComments, bool includeAuthor);
    }
}
