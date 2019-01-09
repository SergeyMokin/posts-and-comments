using PostsAndCommentsModels.Extensions;

namespace PostsAndCommentsModels.DatabaseModels
{
    public class User: IModel<User>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string HashedPassword { get; set; }
        public int? ImageId { get; set; }
        public Image Image { get; set; }
        
        public void Edit(User model)
        {
            FirstName = model.FirstName;
            LastName = model.LastName;
            Email = model.Email;
            Role = model.Role;
            HashedPassword = model.HashedPassword;
        }

        public bool IsValid()
        {
            return !(string.IsNullOrWhiteSpace(FirstName)
                     && string.IsNullOrWhiteSpace(LastName)
                     && string.IsNullOrWhiteSpace(Email))
                   && Role.IsCorrectRole();
        }
    }
}
