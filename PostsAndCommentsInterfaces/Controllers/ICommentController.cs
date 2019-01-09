using System.Collections.Generic;
using System.Threading.Tasks;
using PostsAndCommentsModels.CreationModels;
using PostsAndCommentsModels.ViewModels;

namespace PostsAndCommentsInterfaces.Controllers
{
    public interface ICommentController
    {
        IEnumerable<CommentView> Get(int postId);
        Task<CommentView> Get(int postId, int id);
        Task<CommentView> Add(Comment comment);
        Task<CommentView> Edit(int id, Comment comment);
        Task<CommentView> Delete(int id);
    }
}
