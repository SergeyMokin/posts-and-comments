using System.Linq;
using System.Threading.Tasks;
using Company.PostsAndCommentsModels;

namespace Company.PostsAndCommentsRepositories.Interfaces
{
    public interface IRepository<T>
    where T: class, IModel<T>
    {
        IQueryable<T> Get();
        Task<T> Get(int id);
        Task<T> Edit(T model);
        Task<T> Add(T model);
        Task<T> Delete(int id);
    }
}
