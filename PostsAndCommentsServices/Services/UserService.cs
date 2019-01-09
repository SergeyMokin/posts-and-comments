using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostsAndCommentsInterfaces.Repositories;
using PostsAndCommentsInterfaces.Services;
using PostsAndCommentsModels;
using PostsAndCommentsModels.CreationModels;
using PostsAndCommentsModels.DatabaseModels;
using PostsAndCommentsModels.ViewModels;
using PostsAndCommentsServices.Extensions;

namespace PostsAndCommentsServices.Services
{
    public class UserService: IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IImageService _imageService;
        private readonly IEmailService _emailService;

        public UserService(IRepository<User> userRepository, IImageService imageService, IEmailService emailService)
        {
            _userRepository = userRepository;
            _imageService = imageService;
            _emailService = emailService;
        }

        public async Task<UserToken> Register(RegistrationUser user)
        {
            if(user == null || !user.IsValid()) throw new ArgumentException();

            if (await _userRepository.Get().Include(x => x.Image).FirstOrDefaultAsync(x => x.Email == user.Email) != null)
            {
                throw new InvalidOperationException();
            }

            var img = await _imageService.Add(user.ImageData, user.ImageName);

            var result = await _userRepository.Add(new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                HashedPassword = user.Password.HashPassword(),
                ImageId = img?.Id,
                Role = Roles.User
            });

            return result.GetToken(_emailService);
        }

        public async Task<UserToken> Login(LoginUser user)
        {
            if (user == null || !user.IsValid()) throw new ArgumentException();

            var result = await _userRepository.Get().Include(x => x.Image).FirstOrDefaultAsync(x => x.Email == user.Email)
                ?? throw new InvalidOperationException();

            if (!result.HashedPassword.VerifyHashedPassword(user.Password)) throw new InvalidOperationException();

            return result.GetToken(_emailService);
        }
    }
}
