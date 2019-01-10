using System.Linq;
using System.Threading.Tasks;
using Company.PostsAndCommentsModels.CreationModels;
using Company.PostsAndCommentsModels.ViewModels;

namespace Company.PostsAndCommentsServices.Interfaces
{
    public interface ICommentService
    {
        IQueryable<CommentView> Get(int postId);
        Task<CommentView> Get(int postId, int id);
        Task<CommentView> Add(Comment comment);
        Task<CommentView> Edit(int id, Comment comment);
        Task<CommentView> Delete(int id);
    }
}
