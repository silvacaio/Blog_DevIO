using Blog_DevIO.Domain.Entities;
using Blog_DevIO.Domain.Repositories;

namespace Blog_DevIO.Data.Repositories
{
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        public CommentRepository(BlogContext blogContext) : base(blogContext)
        {
        }
    }
}
