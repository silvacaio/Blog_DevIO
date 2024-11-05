using Blog_DevIO.Core.Data;
using Blog_DevIO.Core.Data.Abstractions;
using Blog_DevIO.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog_DevIO.Data.Repositories
{
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        public CommentRepository(BlogContext blogContext) : base(blogContext)
        {
        }

        public async Task<IEnumerable<Comment?>> GetByPostId(Guid postId, bool includeAuthor = false)
        {
            var query = DbSet.AsNoTracking();
            if (includeAuthor)
                query = query.Include(q => q.Author);

            return await query.Where(c => c.PostId == postId && c.IsDeleted == false).ToListAsync();
        }

        public async Task<Comment?> GetByPostIdAndId(Guid postId, Guid id)
        {
            return await DbSet.
                AsNoTracking()
                .FirstOrDefaultAsync(c => c.PostId == postId && c.Id == id && c.IsDeleted == false);
        }
    }
}
