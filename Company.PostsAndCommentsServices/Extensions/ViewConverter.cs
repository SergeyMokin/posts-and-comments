using System;
using System.Linq;
using Company.PostsAndCommentsModels.DatabaseModels;
using Company.PostsAndCommentsModels.ViewModels;

namespace Company.PostsAndCommentsServices.Extensions
{
    public static class ViewConverter
    {
        public static PostView ToView(this Post post)
        {
            return MapModel(post, x => new PostView
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ImageLink = x.Image?.Link,
                Comments = x.Comments?.Select(c => c.ToView()),
                Likes = x.Likes?.Select(l => l.ToView())
            });
        }

        public static UserView ToView(this User user)
        {
            return MapModel(user, x => new UserView
            {
                Id = x.Id,
                Email = x.Email,
                FullName = x.FirstName + " " + x.LastName,
                ImageLink = x.Image?.Link,
                Role = x.Role
            });
        }

        public static CommentView ToView(this Comment comment)
        {
            return MapModel(comment, x => new CommentView
            {
                Id = x.Id,
                Text = x.Text,
                User = x.User.ToView()
            });
        }

        public static LikeView ToView(this Like like)
        {
            return MapModel(like, x => new LikeView
            {
                Id = x.Id,
                User = x.User.ToView()
            });
        }

        private static TOut MapModel<TIn, TOut>(TIn model, Func<TIn, TOut> map)
        where TOut: class, new()
        where TIn: class
        {
            return model == null ? new TOut() : map(model);
        }
    }
}
