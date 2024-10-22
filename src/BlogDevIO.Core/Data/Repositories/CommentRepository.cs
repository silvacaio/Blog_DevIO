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

        public async Task<IEnumerable<Comment?>> GetByPostId(Guid postId)
        {
            return await DbSet.Where(c => c.PostId == postId).ToListAsync();
        }
    }
}
