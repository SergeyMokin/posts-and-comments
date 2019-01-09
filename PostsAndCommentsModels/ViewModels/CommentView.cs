namespace PostsAndCommentsModels.ViewModels
{
    public class CommentView
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public UserView User { get; set; }
    }
}
