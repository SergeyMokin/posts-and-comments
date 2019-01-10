using System;

namespace Company.PostsAndCommentsModels.DatabaseModels
{
    public class Image: IModel<Image>
    {
        public int Id { get; set; }
        public string Link { get; set; }

        public void Edit(Image model)
        {
            Link = model.Link;
        }

        public bool IsValid()
        {
            return Uri.IsWellFormedUriString(Link, UriKind.Absolute);
        }
    }
}
