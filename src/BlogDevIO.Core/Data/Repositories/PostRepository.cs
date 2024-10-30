using Blog_DevIO.Core.Data;
using Blog_DevIO.Core.Data.Abstractions;
using Blog_DevIO.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog_DevIO.Data.Repositories
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(BlogContext blogContext) : base(blogContext)
        {
        }

        public async Task<IEnumerable<Post?>> GetByUser(Guid authorId)
        {
            return await DbSet.Where(p => p.AuthorId == authorId).ToListAsync();
        }

        public async Task<Post?> Get(Guid id, bool includeComments, bool includeAuthor)
        {
            var query = DbSet.AsNoTracking();

            if (includeAuthor)
                query = query.Include(a => a.Author);

            if (includeComments)
                query = query.Include(a => a.Comments)
                .ThenInclude(a => a.Author);


            return await query.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Post?>> GetAll(bool includeComments, bool includeAuthor)
        {
            var query = DbSet.AsNoTracking();

            if (includeAuthor)
                query = query.Include(a => a.Author);

            if (includeComments)
                query = query.Include(a => a.Comments)
                .ThenInclude(a => a.Author);


            return await query.OrderByDescending(p => p.Creation)
                                .ToListAsync();
        }
    }
}
