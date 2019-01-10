using System.Threading.Tasks;
using Company.PostsAndCommentsModels.CreationModels;
using Company.PostsAndCommentsModels.ViewModels;

namespace Company.PostsAndCommentsServices.Interfaces
{
    public interface IUserService
    {
        Task<UserToken> Register(RegistrationUser user);
        Task<UserToken> Login(LoginUser user);
    }
}
