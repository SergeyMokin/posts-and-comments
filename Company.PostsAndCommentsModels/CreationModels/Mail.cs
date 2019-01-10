using System.ComponentModel.DataAnnotations;
using Company.PostsAndCommentsModels.Extensions;

namespace Company.PostsAndCommentsModels.CreationModels
{
    public class Mail: IValidatable
    {
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string RecipientEmail { get; set; }
        public bool IsValid() => RecipientEmail.IsEmail() && !(string.IsNullOrWhiteSpace(Subject) && string.IsNullOrWhiteSpace(Text));
    }
}
