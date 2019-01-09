using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PostsAndCommentsInterfaces.Repositories;
using PostsAndCommentsInterfaces.Services;
using PostsAndCommentsModels.DatabaseModels;
using PostsAndCommentsModels.ViewModels;
using PostsAndCommentsServices.Extensions;
using DbPost = PostsAndCommentsModels.DatabaseModels.Post;
using Like = PostsAndCommentsModels.DatabaseModels.Like;
using Post = PostsAndCommentsModels.CreationModels.Post;

namespace PostsAndCommentsServices.Services
{
    public class PostService: IPostService
    {
        private readonly IRepository<DbPost> _postRepository;
        private readonly IRepository<Like> _likeRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IImageService _imageService;
        private readonly IHttpContextAccessor _accessor;

        public PostService(IRepository<DbPost> postRepository, 
            IRepository<Like> likeRepository,
            IRepository<User> userRepository,
            IImageService imageService,
            IHttpContextAccessor accessor
            )
        {
            _postRepository = postRepository;
            _likeRepository = likeRepository;
            _userRepository = userRepository;
            _imageService = imageService;
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
            return (await _postRepository.Get()
                    .Include(x => x.Image)
                    .Include(x => x.Comments)
                    .ThenInclude(x => x.User)
                    .ThenInclude(x => x.Image)
                    .Include(x => x.Likes)
                    .ThenInclude(x => x.User)
                    .ThenInclude(x => x.Image)
                    .FirstOrDefaultAsync(x => x.Id == id))
                .ToView();
        }

        public async Task<PostView> Create(Post post)
        {
            if(post == null || !post.IsValid()) throw new ArgumentException();

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
            if (post == null || !post.IsValid()) throw new ArgumentException();

            await _postRepository.Edit(new DbPost
            {
                Id = post.Id ?? throw new ArgumentException(),
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
            var user = await _userRepository.Get(_accessor.HttpContext.User.GetUserId());

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
