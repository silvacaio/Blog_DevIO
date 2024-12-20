﻿using Blog_DevIO.Core.Entities;

namespace Blog_DevIO.Core.Data.Abstractions
{
    public interface ICommentRepository : IRepositoryBase<Comment>
    {
        Task<IEnumerable<Comment?>> GetByPostId(Guid postId, bool includeAuthor = false);
        Task<Comment?> GetByPostIdAndId(Guid postId, Guid id);
    }
}
