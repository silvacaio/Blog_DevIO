
namespace Blog_DevIO.Core.ViewModels.Post
{
    public class PostWithCommentsViewModel : PostViewModel
    {        
        //COmments

        //Author

        public static PostWithCommentsViewModel New(Guid id, string title, string content, DateTime creation, bool canEdit)
        {
            return new PostWithCommentsViewModel
            {
                Id = id.ToString(),
                Title = title,
                Content = content,
                CanEdit = canEdit,
                Creation = creation
            };
        }
    }
}
