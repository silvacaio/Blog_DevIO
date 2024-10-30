
namespace Blog_DevIO.Core.ViewModels.Post
{
    public class PostViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Creation { get; set; }
        public bool CanEdit { get; set; } = false;

        public static PostViewModel New(Guid id, string title, string content, DateTime creation, bool canEdit)
        {
            return new PostViewModel
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
