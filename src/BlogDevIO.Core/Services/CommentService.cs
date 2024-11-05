using Blog_DevIO.Core.Services.Abstractions;
using Blog_DevIO.Core.ViewModels.Comments;
using Blog_DevIO.Core.Entities;
using Blog_DevIO.Core.Data.Abstractions;

namespace Blog_DevIO.Core.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IAppUserService _userService;

        public CommentService(ICommentRepository commentRepository, IAppUserService userService)
        {
            _commentRepository = commentRepository;
            _userService = userService;
        }

        public async Task<IEnumerable<CommentViewModel?>> Get()
        {
            var comments = await _commentRepository.GetAll();
            if (comments == null)
                return Enumerable.Empty<CommentViewModel>();

            var commentsView = new List<CommentViewModel>();
            foreach (var comment in comments)
            {
                commentsView.Add(CreateCommentViewModel(comment));
            }

            return commentsView;
        }

        public async Task<CommentViewModel?> GetById(Guid id)
        {
            var comment = await _commentRepository.Get(id);
            if (comment == null) return null;

            return CreateCommentViewModel(comment);
        }
        public async Task<IEnumerable<CommentViewModel?>> GetByPostId(Guid postId)
        {
            var comments = await _commentRepository.GetByPostId(postId, true);
            if (comments == null)
                return Enumerable.Empty<CommentViewModel>();

            var commentsView = new List<CommentViewModel>();
            foreach (var comment in comments)
            {
                commentsView.Add(CreateCommentViewModel(comment));
            }

            return commentsView;
        }

        public async Task<CommentViewModel?> GetByPostIdAndId(Guid postId, Guid id)
        {
            var comment = await _commentRepository.GetByPostIdAndId(postId, id);
            if (comment == null) return null;

            return CreateCommentViewModel(comment);
        }

        public async Task Create(CreateCommentViewModel comment)
        {
            var userId = _userService.GetId();
            var newComment = new Comment(comment.Content, comment.PostId, Guid.Parse(userId));
            await _commentRepository.Save(newComment);
        }

        public async Task<Comment?> Update(EditCommentViewModel comment)
        {
            var commentToAction = await GetCommentToAction(comment.PostId, comment.Id);
            if (commentToAction == null)
                return null;

            var newComment = new Comment(comment.Id, comment.Content, comment.PostId, commentToAction.AuthorId);
            await _commentRepository.Update(newComment);
            return newComment;
        }

        public async Task Update(CommentViewModel comment)
        {
            var commentToAction = await GetCommentToAction(comment.PostId, comment.Id);
            if (commentToAction == null)
                return;

            var newComment = new Comment(comment.Id, comment.Content, comment.PostId, commentToAction.AuthorId);
            await _commentRepository.Update(newComment);
        }

        public async Task Delete(Guid postid, Guid id)
        {
            var commentToAction = await GetCommentToAction(postid, id);
            if (commentToAction == null)
                return;

            await _commentRepository.Delete(commentToAction);
        }
        public async Task<Comment?> GetCommentToAction(Guid postId, Guid id)
        {
            var commentToEdit = await _commentRepository.GetByPostIdAndId(postId, id);
            if (commentToEdit == null)
                return null;

            if (_userService.IsAdmin() == false || _userService.GetId() != commentToEdit.AuthorId.ToString())
                return null;

            return commentToEdit;
        }
        public CommentViewModel CreateCommentViewModel(Comment comment)
        {
            var canEdit = CanEdit(comment);
            return CommentViewModel.Load(comment.Id, comment.Content, comment.Creation, comment.PostId, comment.Author, canEdit);
        }
        private bool CanEdit(Comment comment)
        {
            return _userService.IsAdmin() == true || _userService.GetId() == comment.AuthorId.ToString();
        }
    }
}
