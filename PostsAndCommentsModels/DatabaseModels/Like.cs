namespace PostsAndCommentsModels.DatabaseModels
{
    public class Like: IModel<Like>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public User User { get; set; }

        public void Edit(Like model) {}

        public bool IsValid() => true;
    }
}
