using System.Collections.Generic;
using System.Threading.Tasks;
using Company.PostsAndCommentsModels.CreationModels;
using Company.PostsAndCommentsModels.ViewModels;

namespace Company.PostsAndComments.Interfaces
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
