using System.Threading.Tasks;
using Company.PostsAndCommentsModels.DatabaseModels;

namespace Company.PostsAndCommentsServices.Interfaces
{
    public interface IImageService
    {
        Task<Image> Get(int id);
        Task<Image> Add(string imageData, string imageName);
        Task<Image> Delete(int id);
    }
}
