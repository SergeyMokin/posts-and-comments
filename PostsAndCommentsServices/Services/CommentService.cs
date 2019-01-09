using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PostsAndCommentsInterfaces.Repositories;
using PostsAndCommentsInterfaces.Services;
using PostsAndCommentsModels.CreationModels;
using PostsAndCommentsModels.ViewModels;
using PostsAndCommentsServices.Extensions;
using DbComment = PostsAndCommentsModels.DatabaseModels.Comment;

namespace PostsAndCommentsServices.Services
{
    public class CommentService: ICommentService
    {
        private readonly IRepository<DbComment> _commentRepository;
        private readonly IHttpContextAccessor _accessor;

        public CommentService(IRepository<DbComment> commentRepository,
            IHttpContextAccessor accessor)
        {
            _commentRepository = commentRepository;
            _accessor = accessor;
        }

        public IQueryable<CommentView> Get(int postId)
        {
            return _commentRepository.Get().Include(x => x.User).ThenInclude(x => x.Image)
                .Where(x => x.PostId == postId).Select(x => x.ToView());
        }

        public async Task<CommentView> Get(int postId, int id)
        {
            var comment = await _commentRepository.Get().Include(x => x.User).ThenInclude(x => x.Image)
                .FirstOrDefaultAsync(x => x.Id == id);
            return comment.PostId == postId
                ? comment.ToView()
                : throw new ArgumentNullException();
        }

        public async Task<CommentView> Add(Comment comment)
        {
            if(comment == null || !comment.IsValid()) throw new ArgumentException();

            return (await _commentRepository.Add(new DbComment
            {
                UserId = _accessor.HttpContext.User.GetUserId(),
                PostId = comment.PostId,
                Text = comment.Text
            })).ToView();
        }

        public async Task<CommentView> Edit(int id, Comment comment)
        {
            if (comment == null || !comment.IsValid()) throw new ArgumentException();

            return (await _commentRepository.Edit(new DbComment
            {
                Id = id,
                PostId = comment.PostId,
                Text = comment.Text
            })).ToView();
        }

        public async Task<CommentView> Delete(int id)
        {
            return (await _commentRepository.Delete(id)).ToView();
        }
    }
}
