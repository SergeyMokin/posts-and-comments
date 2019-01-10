using System.Collections.Generic;
using System.Threading.Tasks;
using Company.PostsAndComments.Interfaces;
using Company.PostsAndCommentsModels;
using Company.PostsAndCommentsModels.CreationModels;
using Company.PostsAndCommentsModels.ViewModels;
using Company.PostsAndCommentsServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.PostsAndComments.Controllers
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
        public async Task<PostView> Get(int id)
        {
            return await _postService.Get(id);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<PostView> Delete(int id)
        {
            return await _postService.Delete(id);
        }

        [HttpPost]
        public async Task<PostView> Create([FromBody] Post post)
        {
            return await _postService.Create(post);
        }

        [HttpPut]
        public async Task<PostView> Edit([FromBody] Post post)
        {
            return await _postService.Edit(post);
        }

        [HttpPost]
        [Route("[action]/{id}")]
        public async Task<PostView> LikePost(int id)
        {
            return await _postService.LikePost(id);
        }
    }
}
