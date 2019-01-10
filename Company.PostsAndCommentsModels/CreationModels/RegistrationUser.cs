using System.ComponentModel.DataAnnotations;
using Company.PostsAndCommentsModels.Extensions;

namespace Company.PostsAndCommentsModels.CreationModels
{
    public class RegistrationUser: IValidatable
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ImageData { get; set; }
        public string ImageName { get; set; }

        public bool IsValid()
        {
            return !(string.IsNullOrWhiteSpace(FirstName)
                && string.IsNullOrWhiteSpace(LastName))
                && Email.IsEmail()
                && Password.IsPassword();
        }
    }
}
