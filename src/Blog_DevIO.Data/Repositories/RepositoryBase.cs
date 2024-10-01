using Blog_DevIO.Domain.Entities;
using Blog_DevIO.Domain.Repositories;
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

        public Task Delete(T entity)
        {
            return Task.FromResult(DbSet.Remove(entity));
        }

        public async Task<T?> Get(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T?>> GetAll()
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
            DbSet.Update(entity);
            await _blogContext.SaveChangesAsync();
        }
    }
}
