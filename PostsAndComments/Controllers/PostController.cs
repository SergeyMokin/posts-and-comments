using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostsAndCommentsInterfaces.Controllers;
using PostsAndCommentsInterfaces.Services;
using PostsAndCommentsModels;
using PostsAndCommentsModels.CreationModels;
using PostsAndCommentsModels.ViewModels;

namespace PostsAndComments.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PostController: Controller, IPostController
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        
        public IEnumerable<PostView> Get()
        {
            return _postService.Get();
        }
        
        [Route("{id}")]
        //{host}/api/post/{id}
        public async Task<PostView> Get(int id)
        {
            return await _postService.Get(id);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = Roles.Admin)]
        //{host}/api/post/{id}
        public async Task<PostView> Delete(int id)
        {
            return await _postService.Delete(id);
        }

        [HttpPost]
        //{host}/api/post
        public async Task<PostView> Create([FromBody] Post post)
        {
            return await _postService.Create(post);
        }

        [HttpPut]
        //{host}/api/post
        public async Task<PostView> Edit([FromBody] Post post)
        {
            return await _postService.Edit(post);
        }

        [HttpPost]
        [Route("[action]/{id}")]
        //{host}/api/post/LikePost/{id}
        public async Task<PostView> LikePost(int id)
        {
            return await _postService.LikePost(id);
        }
    }
}
