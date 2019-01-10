using System.Threading.Tasks;
using Company.PostsAndComments.Interfaces;
using Company.PostsAndCommentsModels.CreationModels;
using Company.PostsAndCommentsModels.ViewModels;
using Company.PostsAndCommentsServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Company.PostsAndComments.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class UserController: Controller, IUserController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<UserToken> Register([FromBody] RegistrationUser user)
        {
            return await _userService.Register(user);
        }

        [HttpPost]
        public async Task<UserToken> Login([FromBody] LoginUser user)
        {
            return await _userService.Login(user);
        }
    }
}
