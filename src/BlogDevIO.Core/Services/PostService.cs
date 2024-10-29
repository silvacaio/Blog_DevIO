using Blog_DevIO.Core.Services.Abstractions;
using Blog_DevIO.Core.Entities;
using Blog_DevIO.Core.Data.Abstractions;
using Blog_DevIO.Core.ViewModels.Post;
namespace Blog_DevIO.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IAppUserService _userService;
        public PostService(IPostRepository postRepository, IAppUserService userService)
        {
            _postRepository = postRepository;
            _userService = userService;
        }

        public async Task<IEnumerable<PostViewModel?>> Get()
        {
            var posts = await _postRepository.GetAll();
            if (posts == null)
                return Enumerable.Empty<PostViewModel>();

            var postsView = new List<PostViewModel>();
            foreach (var post in posts)
            {
                postsView.Add(CreatePostViewModel(post));
            }

            return postsView;
        }

        public async Task<PostViewModel?> GetById(Guid id)
        {
            var post = await _postRepository.Get(id);
            if (post == null)
                return null;

            return CreatePostViewModel(post);
        }

        public async Task<PostWithCommentsAndAuthorViewModel?> GetPostWithCommentsAndAuthorById(Guid id)
        {
            var post = await _postRepository.Get(id);
            if (post == null)
                return null;

            var canEdit = CanEditPost(post);

            var postViewModel = PostWithCommentsAndAuthorViewModel.New(
                post.Id,
                post.Title,
                post.Content,
                post.Creation,
                canEdit,
                post.Author,
                post.Comments);

            return postViewModel;
        }

        public Task<IEnumerable<Post?>> GetByUser()
        {
            var userId = _userService.GetId();
            return _postRepository.GetByUser(Guid.Parse(userId));
        }

        public async Task Create(CreatePostViewModel post)
        {
            var userId = _userService.GetId();
            var newPost = new Post(post.Title, post.Content, Guid.Parse(userId));
            await _postRepository.Save(newPost);
        }

        public async Task<Post?> Update(EditPostViewModel post)
        {
            var postToAction = await GetPostToAction(post.Id);
            if (postToAction == null)
                return null;

            var newPost = new Post(post.Id, post.Title, post.Content, postToAction.AuthorId);
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

        public async Task<Post?> GetPostToAction(Guid id)
        {
            var postToEdit = await _postRepository.Get(id);
            if (postToEdit == null)
                return null;

            if (CanEditPost(postToEdit) == false)
                return null;

            return postToEdit;
        }

        private bool CanEditPost(Post post)
        {
            return _userService.IsAdmin() == true || _userService.GetId() == post.AuthorId.ToString();
        }

        private PostViewModel CreatePostViewModel(Post post)
        {
            var canEdit = CanEditPost(post);
            return PostViewModel.New(post.Id, post.Title, post.Content, post.Creation, canEdit);
        }
    }
}
