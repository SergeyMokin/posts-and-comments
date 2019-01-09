using System.Collections.Generic;
using System.Threading.Tasks;
using PostsAndCommentsModels.CreationModels;
using PostsAndCommentsModels.ViewModels;

namespace PostsAndCommentsInterfaces.Controllers
{
    public interface IPostController
    {
        IEnumerable<PostView> Get();
        Task<PostView> Get(int id);
        Task<PostView> Create(Post post);
        Task<PostView> Edit(Post post);
        Task<PostView> Delete(int id);
        Task<PostView> LikePost(int id);
    }
}
