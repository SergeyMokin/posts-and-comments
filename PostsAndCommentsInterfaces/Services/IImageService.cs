using System.Threading.Tasks;
using PostsAndCommentsModels.DatabaseModels;

namespace PostsAndCommentsInterfaces.Services
{
    public interface IImageService
    {
        Task<Image> Get(int id);
        Task<Image> Add(string imageData, string imageName);
        Task<Image> Delete(int id);
    }
}
