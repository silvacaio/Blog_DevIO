namespace Blog_DevIO.Domain.ViewModels.Post
{
    public class CreatePostViewlModel
    {
        public string Title { get; private set; }
        public string Content { get; private set; }
        public string[]? Tags { get; private set; }
    }
}
