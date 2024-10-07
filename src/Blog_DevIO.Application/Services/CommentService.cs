using Blog_DevIO.Application.Services.Abstractions;
using Blog_DevIO.Application.ViewModels.Comments;
using Blog_DevIO.Domain.Entities;
using Blog_DevIO.Domain.Repositories;

namespace Blog_DevIO.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<IEnumerable<Comment?>> Get()
        {
            return await _commentRepository.GetAll();
        }

        public async Task<Comment?> GetById(Guid id)
        {
            return await _commentRepository.Get(id);
        }
        public async Task Create(CreateCommentViewModel comment, string userId)
        {
            var newComment = new Comment(comment.Content, comment.PostId, userId);
            await _commentRepository.Save(newComment);
        }

        public async Task<Comment?> Update(EditCommentViewModel comment, string userId)
        {
            var commentToEdit = await GetById(comment.Id);
            if (commentToEdit == null)
                return null;

            if (commentToEdit.UserId != userId)
                return null;

            if (commentToEdit.Id != comment.Id)
                return null;


            var newComment = new Comment(comment.Id, comment.Content, comment.PostId, userId);
            await _commentRepository.Update(newComment);

            return newComment;
        }

        public async Task<Comment?> Delete(Guid id)
        {
            var Comment = await GetById(id);
            if (Comment == null)
                return null;

            await _commentRepository.Delete(Comment);

            return null;
        }
    }
}
