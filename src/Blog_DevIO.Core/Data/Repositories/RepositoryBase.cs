using Blog_DevIO.Core.Data;
using Blog_DevIO.Core.Data.Abstractions;
using Blog_DevIO.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog_DevIO.Data.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        protected readonly BlogContext _blogContext;
        protected readonly DbSet<T> DbSet;

        public RepositoryBase(BlogContext blogContext)
        {
            _blogContext = blogContext;
            DbSet = _blogContext.Set<T>();
        }

        public async Task Delete(T entity)
        {
            DbSet.Remove(entity);
            await _blogContext.SaveChangesAsync();
        }

        public async Task<T?> Get(Guid id)
        {
            return await DbSet
                .AsNoTracking().
                FirstOrDefaultAsync(d => d.Id == id);
        }

        public virtual async Task<IEnumerable<T?>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public async Task Save(T entity)
        {
            DbSet.Add(entity);
            await _blogContext.SaveChangesAsync();
        }
        public async Task Update(T entity)
        {
            DbSet.Attach(entity);
            DbSet.Entry(entity).State = EntityState.Modified;
            await _blogContext.SaveChangesAsync();
        }
    }
}
