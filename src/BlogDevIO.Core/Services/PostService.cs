using Blog_DevIO.Core.Services.Abstractions;
using Blog_DevIO.Core.Entities;
using Blog_DevIO.Core.Data.Abstractions;
using Blog_DevIO.Core.ViewModels.Post;
namespace Blog_DevIO.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserService _userService;
        public PostService(IPostRepository postRepository, IUserService userService)
        {
            _postRepository = postRepository;
            _userService = userService;
        }

        public async Task<IEnumerable<Post?>> Get()
        {
            return await _postRepository.GetAll();
        }

        public async Task<Post?> GetById(Guid id)
        {
            return await _postRepository.Get(id);
        }

        public Task<IEnumerable<Post?>> GetByUser()
        {
            var userId = _userService.GetId();
            return _postRepository.GetByUser(userId);
        }

        public async Task Create(CreatePostViewModel post)
        {
            var userId = _userService.GetId();
            var newPost = new Post(post.Title, post.Content, userId);
            await _postRepository.Save(newPost);
        }

        public async Task<Post?> Update(EditPostViewModel post)
        {
            var postToAction = await GetPostToAction(post.Id);
            if (postToAction == null)
                return null;

            var newPost = new Post(post.Id, post.Title, post.Content, postToAction.UserId);
            await _postRepository.Update(newPost);

            return newPost;
        }

        public async Task Delete(Guid id)
        {
            var postToAction = await GetPostToAction(id);
            if (postToAction == null)
                return;

            await _postRepository.Delete(postToAction);
        }

        public async Task<Post?> GetPostToAction(Guid postId)
        {
            var postToEdit = await GetById(postId);
            if (postToEdit == null)
                return null;

            if (_userService.IsAdmin() == false || _userService.GetId() != postToEdit.UserId)
                return null;

            return postToEdit;
        }
    }
}
