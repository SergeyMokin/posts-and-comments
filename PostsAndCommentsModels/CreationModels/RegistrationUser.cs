using PostsAndCommentsModels.Extensions;

namespace PostsAndCommentsModels.CreationModels
{
    public class RegistrationUser: IValidated
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Base64.
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
