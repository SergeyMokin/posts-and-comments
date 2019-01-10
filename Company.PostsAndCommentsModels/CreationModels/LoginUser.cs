using System.ComponentModel.DataAnnotations;
using Company.PostsAndCommentsModels.Extensions;

namespace Company.PostsAndCommentsModels.CreationModels
{
    public class LoginUser: IValidatable
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsValid()
        {
            return Email.IsEmail() && Password.IsPassword();
        }
    }
}
