using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PostsAndCommentsInterfaces.Controllers;
using PostsAndCommentsInterfaces.Services;
using PostsAndCommentsModels.CreationModels;
using PostsAndCommentsModels.ViewModels;

namespace PostsAndComments.Controllers
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
        //api/user/register
        public async Task<UserToken> Register([FromBody] RegistrationUser user)
        {
            return await _userService.Register(user);
        }

        [HttpPost]
        //api/user/login
        public async Task<UserToken> Login([FromBody] LoginUser user)
        {
            return await _userService.Login(user);
        }
    }
}
