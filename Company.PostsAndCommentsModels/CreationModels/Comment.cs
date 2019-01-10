using System.ComponentModel.DataAnnotations;

namespace Company.PostsAndCommentsModels.CreationModels
{
    public class Comment: IValidatable
    {
        [Required]
        public int PostId { get; set; }
        [Required]
        public string Text { get; set; }
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Text);
        }
    }
}
