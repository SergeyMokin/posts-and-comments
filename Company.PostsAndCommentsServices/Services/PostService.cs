using System.Linq;
using System.Threading.Tasks;
using Company.PostsAndCommentsModels.CustomExceptions;
using Company.PostsAndCommentsModels.DatabaseModels;
using Company.PostsAndCommentsModels.ViewModels;
using Company.PostsAndCommentsRepositories.Interfaces;
using Company.PostsAndCommentsServices.Extensions;
using Company.PostsAndCommentsServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using DbPost = Company.PostsAndCommentsModels.DatabaseModels.Post;
using Like = Company.PostsAndCommentsModels.DatabaseModels.Like;
using Post = Company.PostsAndCommentsModels.CreationModels.Post;

namespace Company.PostsAndCommentsServices.Services
{
    public class PostService: IPostService
    {
        private readonly IRepository<DbPost> _postRepository;
        private readonly IRepository<Like> _likeRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IImageService _imageService;
        private readonly IAuthorizeService _authorizeService;
        private readonly IHttpContextAccessor _accessor;

        public PostService(IRepository<DbPost> postRepository, 
            IRepository<Like> likeRepository,
            IRepository<User> userRepository,
            IImageService imageService,
            IAuthorizeService authorizeService,
            IHttpContextAccessor accessor
            )
        {
            _postRepository = postRepository;
            _likeRepository = likeRepository;
            _userRepository = userRepository;
            _imageService = imageService;
            _authorizeService = authorizeService;
            _accessor = accessor;
        }

        public IQueryable<PostView> Get()
        {
            return _postRepository
                .Get()
                .Include(x => x.Image)
                .Include(x => x.Comments)
                .ThenInclude(x => x.User)
                .ThenInclude(x => x.Image)
                .Include(x => x.Likes)
                .ThenInclude(x => x.User)
                .ThenInclude(x => x.Image)
                .Select(x => x.ToView());
        }

        public async Task<PostView> Get(int id)
        {
            var post = await _postRepository.Get()
                .Include(x => x.Image)
                .Include(x => x.Comments)
                .ThenInclude(x => x.User)
                .ThenInclude(x => x.Image)
                .Include(x => x.Likes)
                .ThenInclude(x => x.User)
                .ThenInclude(x => x.Image)
                .FirstOrDefaultAsync(x => x.Id == id);
            return post != null ? post.ToView() : throw new PacNotFoundException();
        }

        public async Task<PostView> Create(Post post)
        {
            if(post == null || !post.IsValid()) throw new PacInvalidModelException();

            var img = await _imageService.Add(post.ImageData, post.ImageName);

            return (await _postRepository.Add(new DbPost
            {
                Title = post.Title,
                Description = post.Description,
                ImageId = img.Id
            })).ToView();
        }

        public async Task<PostView> Edit(Post post)
        {
            if (post == null || !post.IsValid()) throw new PacInvalidModelException();

            await _postRepository.Edit(new DbPost
            {
                Id = post.Id ?? throw new PacInvalidModelException(),
                Title = post.Title,
                Description = post.Description
            });

            return await Get(post.Id.Value);
        }

        public async Task<PostView> Delete(int id)
        {
            return (await _postRepository.Delete(id)).ToView();
        }

        public async Task<PostView> LikePost(int id)
        {
            var user = await _userRepository.Get(_authorizeService.GetUserId(_accessor.HttpContext.User));

            var like = _likeRepository.Get().FirstOrDefault(x => x.PostId == id && x.UserId == user.Id);

            if (like == null)
            {
                await _likeRepository.Add(new Like {UserId = user.Id, PostId = id});
            }
            else
            {
                await _likeRepository.Delete(like.Id);
            }

            return await Get(id);
        }
    }
}
