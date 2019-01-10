using System.Security.Claims;
using Company.PostsAndCommentsModels.DatabaseModels;
using Company.PostsAndCommentsModels.ViewModels;

namespace Company.PostsAndCommentsServices.Interfaces
{
    public interface IAuthorizeService
    {
        int GetUserId(ClaimsPrincipal user);
        UserToken GetToken(User user, IEmailService emailService = null);
        string HashPassword(string password);
        bool VerifyHashedPassword(string hashedPassword, string password);
    }
}
