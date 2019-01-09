using System.Threading.Tasks;
using PostsAndCommentsModels.CreationModels;
using PostsAndCommentsModels.ViewModels;

namespace PostsAndCommentsInterfaces.Controllers
{
    public interface IUserController
    {
        Task<UserToken> Register(RegistrationUser user);
        Task<UserToken> Login(LoginUser user);
    }
}
