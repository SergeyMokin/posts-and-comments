using PostsAndCommentsModels.Extensions;

namespace PostsAndCommentsModels.CreationModels
{
    public class LoginUser: IValidated
    {

        public string Email { get; set; }
        public string Password { get; set; }

        public bool IsValid()
        {
            return Email.IsEmail() && Password.IsPassword();
        }
    }
}
