using System.Linq;
using System.Threading.Tasks;
using Company.PostsAndCommentsModels.CreationModels;
using Company.PostsAndCommentsModels.CustomExceptions;
using Company.PostsAndCommentsModels.ViewModels;
using Company.PostsAndCommentsRepositories.Interfaces;
using Company.PostsAndCommentsServices.Extensions;
using Company.PostsAndCommentsServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using DbComment = Company.PostsAndCommentsModels.DatabaseModels.Comment;

namespace Company.PostsAndCommentsServices.Services
{
    public class CommentService: ICommentService
    {
        private readonly IRepository<DbComment> _commentRepository;
        private readonly IAuthorizeService _authorizeService;
        private readonly IHttpContextAccessor _accessor;

        public CommentService(IRepository<DbComment> commentRepository,
            IAuthorizeService authorizeService,
            IHttpContextAccessor accessor)
        {
            _commentRepository = commentRepository;
            _authorizeService = authorizeService;
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
                : throw new PacNotFoundException();
        }

        public async Task<CommentView> Add(Comment comment)
        {
            if(comment == null || !comment.IsValid()) throw new PacInvalidModelException();

            return (await _commentRepository.Add(new DbComment
            {
                UserId = _authorizeService.GetUserId(_accessor.HttpContext.User),
                PostId = comment.PostId,
                Text = comment.Text
            })).ToView();
        }

        public async Task<CommentView> Edit(int id, Comment comment)
        {
            if (comment == null || !comment.IsValid()) throw new PacInvalidModelException();

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
