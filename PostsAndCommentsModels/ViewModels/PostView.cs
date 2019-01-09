using System.Collections.Generic;

namespace PostsAndCommentsModels.ViewModels
{
    public class PostView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageLink { get; set; }

        public IEnumerable<LikeView> Likes { get; set; }
        public IEnumerable<CommentView> Comments { get; set; }
    }
}
