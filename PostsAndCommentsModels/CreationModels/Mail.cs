using PostsAndCommentsModels.Extensions;

namespace PostsAndCommentsModels.CreationModels
{
    public class Mail: IValidated
    {
        public string Subject { get; set; }
        public string Text { get; set; }
        public string RecipientEmail { get; set; }
        public bool IsValid() => RecipientEmail.IsEmail() && !(string.IsNullOrWhiteSpace(Subject) && string.IsNullOrWhiteSpace(Text));
    }
}
