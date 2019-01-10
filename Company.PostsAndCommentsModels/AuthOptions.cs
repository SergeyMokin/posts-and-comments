using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Company.PostsAndCommentsModels
{
    public class AuthOptions
    {
        private const string Key = "lkjohn98324mcvbu/';ly!af./23468241bsdf!@enfqife2053631.,,dfas;d@!";
        
        public const int LifeTime = 31;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
