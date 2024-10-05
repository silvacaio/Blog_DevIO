using Blog_DevIO.Application.Services.Abstractions;
using Blog_DevIO.Domain.Entities;
using Blog_DevIO.Domain.Repositories;

namespace Blog_DevIO.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<Post?>> Get()
        {
            return await _postRepository.GetAll();
        }

        public async Task<Post?> GetById(Guid id)
        {
            return await _postRepository.Get(id);
        }

        public async Task<Post?> Delete(Guid id)
        {
            var post = await GetById(id);
            if (post == null)
                return null;

            await _postRepository.Delete(post);

            return null;
        }
    }
}
