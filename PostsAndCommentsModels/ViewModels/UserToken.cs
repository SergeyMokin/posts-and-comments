using System;

namespace PostsAndCommentsModels.ViewModels
{
    public class UserToken
    {
        public UserView User { get; set; }
        public string Token { get; set; }
        public DateTime DateExpires { get; set; }
    }
}
