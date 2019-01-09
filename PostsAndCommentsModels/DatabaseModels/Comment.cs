namespace PostsAndCommentsModels.DatabaseModels
{
    public class Comment: IModel<Comment>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Text { get; set; }
        public User User { get; set; }

        public void Edit(Comment model)
        {
            Text = model.Text;
        }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Text);
        }
    }
}
