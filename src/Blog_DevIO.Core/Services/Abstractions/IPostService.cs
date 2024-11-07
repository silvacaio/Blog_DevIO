using Blog_DevIO.Core.Entities;
using Blog_DevIO.Core.ViewModels.Post;

namespace Blog_DevIO.Core.Services.Abstractions
{
    public interface IPostService
    {
        Task<IEnumerable<PostViewModel?>> Get();
        Task<PostViewModel?> GetById(Guid id);
        Task<PostWithCommentsAndAuthorViewModel?> GetPostWithCommentsAndAuthorById(Guid id);
        Task<IEnumerable<PostWithCommentsAndAuthorViewModel>?> GetWithCommentsAndAuthorById();
        Task<IEnumerable<PostViewModel?>> GetByUser();
        Task Create(CreatePostViewModel post);
        Task<Post?> Update(PostViewModel comment);
        Task Delete(Guid id);
        Task<Post?> GetPostToAction(Guid id);

        ICommentService CommentService { get; }
    }
}
