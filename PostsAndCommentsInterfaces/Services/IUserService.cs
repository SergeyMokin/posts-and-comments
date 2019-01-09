using System.Threading.Tasks;
using PostsAndCommentsModels.CreationModels;
using PostsAndCommentsModels.ViewModels;

namespace PostsAndCommentsInterfaces.Services
{
    public interface IUserService
    {
        Task<UserToken> Register(RegistrationUser user);
        Task<UserToken> Login(LoginUser user);
    }
}
