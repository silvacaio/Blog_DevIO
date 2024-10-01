using Blog_DevIO.Domain.Entities;
using Blog_DevIO.Domain.Repositories;

namespace Blog_DevIO.Data.Repositories
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(BlogContext blogContext) : base(blogContext)
        {
        }
    }
}
