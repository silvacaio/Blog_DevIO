using Blog_DevIO.Core.Services.Abstractions;
using Blog_DevIO.Core.Entities;
using Blog_DevIO.Core.Data.Abstractions;
using Blog_DevIO.Core.ViewModels.Post;
using Blog_DevIO.Core.ViewModels.Authors;
namespace Blog_DevIO.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IAppUserService _userService;
        private readonly ICommentService _commentService;
        public ICommentService CommentService => _commentService;

        public PostService(IPostRepository postRepository, IAppUserService userService, ICommentService commentService)
        {
            _postRepository = postRepository;
            _userService = userService;
            _commentService = commentService;
        }

        public async Task<IEnumerable<PostViewModel?>> Get()
        {
            var posts = await _postRepository.GetAll();
            return ConvertPostsToPostsViewModel(posts);
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
            var post = await _postRepository.Get(id, true, true);
            if (post == null)
                return null;

            var canEdit = CanEditPost(post);

            var author = AuthorViewModel.Load(post.Author);
            var comments = post.Comments?.AsParallel()
                .Select(c => _commentService.CreateCommentViewModel(c)).ToArray();

            var postViewModel = PostWithCommentsAndAuthorViewModel.New(
                post.Id,
                post.Title,
                post.Content,
                post.Creation,
                canEdit,
                author,
                comments);

            return postViewModel;
        }

        public async Task<IEnumerable<PostViewModel>?> GetByUser()
        {
            var userId = _userService.GetId();
            var posts = await _postRepository.GetByUser(Guid.Parse(userId));

            return ConvertPostsToPostsViewModel(posts);
        }

        public async Task Create(CreatePostViewModel post)
        {
            var userId = _userService.GetId();
            var newPost = new Post(post.Title, post.Content, Guid.Parse(userId));
            await _postRepository.Save(newPost);
        }

        public async Task<Post?> Update(PostViewModel post)
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

        private IEnumerable<PostViewModel?> ConvertPostsToPostsViewModel(IEnumerable<Post>? posts)
        {
            if (posts == null)
                return Enumerable.Empty<PostViewModel>();

            var postsView = new List<PostViewModel>();
            foreach (var post in posts)
            {
                postsView.Add(CreatePostViewModel(post));
            }

            return postsView;
        }
    }
}
