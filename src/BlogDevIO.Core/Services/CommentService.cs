using Blog_DevIO.Core.Services.Abstractions;
using Blog_DevIO.Core.ViewModels.Comments;
using Blog_DevIO.Core.Entities;
using Blog_DevIO.Core.Data.Abstractions;

namespace Blog_DevIO.Core.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserService _userService;

        public CommentService(ICommentRepository commentRepository, IUserService userService)
        {
            _commentRepository = commentRepository;
            _userService = userService;
        }

        public async Task<IEnumerable<Comment?>> Get()
        {
            return await _commentRepository.GetAll();
        }

        public async Task<Comment?> GetById(Guid id)
        {
            return await _commentRepository.Get(id);
        }
        public async Task<IEnumerable<Comment?>> GetByPostId(Guid postId)
        {
            return await _commentRepository.GetByPostId(postId);
        }
        public async Task Create(CreateCommentViewModel comment)
        {
            var userId = _userService.GetId();
            var newComment = new Comment(comment.Content, comment.PostId, userId);
            await _commentRepository.Save(newComment);
        }

        public async Task<Comment?> Update(EditCommentViewModel comment)
        {
            var commentToAction = await GetCommentToAction(comment.Id);
            if (commentToAction == null)
                return null;

            var newComment = new Comment(comment.Id, comment.Content, comment.PostId, commentToAction.UserId);
            await _commentRepository.Update(newComment);
            return newComment;
        }

        public async Task Delete(Guid id)
        {
            var commentToAction = await GetCommentToAction(id);
            if (commentToAction == null)
                return;

            await _commentRepository.Delete(commentToAction);
        }

        public async Task<Comment?> GetCommentToAction(Guid commentId)
        {
            var commentToEdit = await GetById(commentId);
            if (commentToEdit == null)
                return null;

            if (_userService.IsAdmin() == false || _userService.GetId() != commentToEdit.UserId)
                return null;

            return commentToEdit;
        }
    }
}
