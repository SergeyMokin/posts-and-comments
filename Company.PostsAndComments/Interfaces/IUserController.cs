using System.Threading.Tasks;
using Company.PostsAndCommentsModels.CreationModels;
using Company.PostsAndCommentsModels.ViewModels;

namespace Company.PostsAndComments.Interfaces
{
    public interface IUserController
    {
        Task<UserToken> Register(RegistrationUser user);
        Task<UserToken> Login(LoginUser user);
    }
}
