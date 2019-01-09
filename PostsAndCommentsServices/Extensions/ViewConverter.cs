using System.Linq;
using PostsAndCommentsModels.DatabaseModels;
using PostsAndCommentsModels.ViewModels;

namespace PostsAndCommentsServices.Extensions
{
    public static class ViewConverter
    {
        public static PostView ToView(this Post post)
        {
            return post == null
                ? null
                : new PostView
                {
                    Id = post.Id,
                    Title = post.Title,
                    Description = post.Description,
                    ImageLink = post.Image?.Link,
                    Comments = post.Comments?.Select(x => x.ToView()),
                    Likes = post.Likes?.Select(x => x.ToView())
                };
        }

        public static UserView ToView(this User user)
        {
            return user == null
                ? null
                : new UserView
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.FirstName + " " + user.LastName,
                    ImageLink = user.Image?.Link,
                    Role = user.Role
                };
        }

        public static CommentView ToView(this Comment comment)
        {
            return comment == null
                ? null
                : new CommentView
                {
                    Id = comment.Id,
                    Text = comment.Text,
                    User = comment.User.ToView()
                };
        }

        public static LikeView ToView(this Like like)
        {
            return like == null
                ? null
                : new LikeView
                {
                    Id = like.Id,
                    User = like.User.ToView()
                };
        }
    }
}
