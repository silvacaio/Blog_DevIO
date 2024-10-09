using Blog_DevIO.Domain.Entities;
using Blog_DevIO.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Blog_DevIO.Data.Repositories
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(BlogContext blogContext) : base(blogContext)
        {
        }

        public async Task<IEnumerable<Post?>> GetByUser(string userId)
        {
            return await DbSet.Where(p => p.UserId == userId).ToListAsync();
        }
    }
}
