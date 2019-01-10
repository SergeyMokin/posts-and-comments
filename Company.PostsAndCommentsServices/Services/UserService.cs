using System.Threading.Tasks;
using Company.PostsAndCommentsModels;
using Company.PostsAndCommentsModels.CreationModels;
using Company.PostsAndCommentsModels.CustomExceptions;
using Company.PostsAndCommentsModels.DatabaseModels;
using Company.PostsAndCommentsModels.ViewModels;
using Company.PostsAndCommentsRepositories.Interfaces;
using Company.PostsAndCommentsServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Company.PostsAndCommentsServices.Services
{
    public class UserService: IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IImageService _imageService;
        private readonly IEmailService _emailService;
        private readonly IAuthorizeService _authorizeService;

        public UserService(IRepository<User> userRepository,
            IImageService imageService,
            IEmailService emailService,
            IAuthorizeService authorizeService)
        {
            _userRepository = userRepository;
            _imageService = imageService;
            _emailService = emailService;
            _authorizeService = authorizeService;
        }

        public async Task<UserToken> Register(RegistrationUser user)
        {
            if(user == null || !user.IsValid()) throw new PacInvalidModelException();

            if (await _userRepository.Get().Include(x => x.Image).FirstOrDefaultAsync(x => x.Email == user.Email) != null)
            {
                throw new PacInvalidOperationException();
            }

            var img = await _imageService.Add(user.ImageData, user.ImageName);

            var result = await _userRepository.Add(new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                HashedPassword = _authorizeService.HashPassword(user.Password),
                ImageId = img?.Id,
                Role = Roles.User
            });

            return _authorizeService.GetToken(result, _emailService);
        }

        public async Task<UserToken> Login(LoginUser user)
        {
            if (user == null || !user.IsValid()) throw new PacInvalidModelException();

            var result = await _userRepository.Get().Include(x => x.Image).FirstOrDefaultAsync(x => x.Email == user.Email)
                ?? throw new PacNotFoundException();

            if (!_authorizeService.VerifyHashedPassword(result.HashedPassword, user.Password)) throw new PacInvalidOperationException();

            return _authorizeService.GetToken(result, _emailService);
        }
    }
}
