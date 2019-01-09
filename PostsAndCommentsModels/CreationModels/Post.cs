namespace PostsAndCommentsModels.CreationModels
{
    public class Post: IValidated
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        // Base64.
        public string ImageData { get; set; }
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
