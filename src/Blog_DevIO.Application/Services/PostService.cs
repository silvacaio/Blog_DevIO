using Blog_DevIO.Application.Services.Abstractions;
using Blog_DevIO.Domain.Entities;
using Blog_DevIO.Domain.Repositories;
using Blog_DevIO.Application.ViewModels.Post;
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

        public Task<IEnumerable<Post?>> GetByUser(string userId)
        {
            return _postRepository.GetByUser(userId);
        }

        public async Task Create(CreatePostViewModel post, string userId)
        {
            var newPost = new Post(post.Title, post.Content, userId);
            await _postRepository.Save(newPost);
        }

        public async Task<Post?> Update(EditPostViewModel post, string userId)
        {
            var postToEdit = await GetById(post.Id);
            if (postToEdit == null)
                return null;

            if (postToEdit.UserId != userId)
                return null;

            if (postToEdit.Id != post.Id)
                return null;


            var newPost = new Post(post.Id, post.Title, post.Content, userId);
            await _postRepository.Update(newPost);

            return newPost;
        }

        public async Task Delete(Guid id)
        {
            var post = await GetById(id);
            if (post == null)
                return;

            await _postRepository.Delete(post);
        }
    }
}
