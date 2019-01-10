using System.Collections.Generic;

namespace Company.PostsAndCommentsModels.DatabaseModels
{
    public class Post: IModel<Post>
    {
        public Post()
        {
            Comments = new List<Comment>();
            Likes = new List<Like>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ImageId { get; set; }
        public Image Image { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<Like> Likes { get; set; }

        public void Edit(Post model)
        {
            Title = model.Title;
            Description = model.Description;
        }

        public bool IsValid()
        {
            return !(string.IsNullOrWhiteSpace(Title) && string.IsNullOrWhiteSpace(Description));
        }
    }
}
