namespace PostsAndCommentsModels.CreationModels
{
    public class Comment: IValidated
    {
        public int PostId { get; set; }
        public string Text { get; set; }
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Text);
        }
    }
}
