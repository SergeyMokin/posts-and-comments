using System.ComponentModel.DataAnnotations;

namespace Company.PostsAndCommentsModels.CreationModels
{
    public class Post: IValidatable
    {
        public int? Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ImageData { get; set; }
        [Required]
        public string ImageName { get; set; }

        public bool IsValid()
        {
            return !(string.IsNullOrWhiteSpace(Title)
                     && string.IsNullOrWhiteSpace(Description)
                     && string.IsNullOrWhiteSpace(ImageData)
                     && string.IsNullOrWhiteSpace(ImageName));
        }
    }
}
