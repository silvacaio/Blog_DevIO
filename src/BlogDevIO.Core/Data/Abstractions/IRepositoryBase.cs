﻿using Blog_DevIO.Core.Entities;

namespace Blog_DevIO.Core.Data.Abstractions
{
    public interface IRepositoryBase<T> where T : EntityBase
    {
        public Task Save(T entity);
        public Task Update(T entity);
        public Task Delete(T entity);
        public Task<T?> Get(Guid id);
        public Task<IEnumerable<T?>> GetAll();
    }
}
