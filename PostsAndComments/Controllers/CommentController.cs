using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostsAndCommentsInterfaces.Controllers;
using PostsAndCommentsInterfaces.Services;
using PostsAndCommentsModels.CreationModels;
using PostsAndCommentsModels.ViewModels;

namespace PostsAndComments.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CommentController: Controller, ICommentController
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }


        [Route("{postId}")]
        //{host}/api/comment/{postId}
        public IEnumerable<CommentView> Get(int postId)
        {
            return _commentService.Get(postId);
        }

        [Route("{postId}/{id}")]
        //{host}/api/comment/{postId}/{id}
        public async Task<CommentView> Get(int postId, int id)
        {
            return await _commentService.Get(postId, id);
        }

        [HttpDelete]
        [Route("{id}")]
        //{host}/api/comment/{id}
        public async Task<CommentView> Delete(int id)
        {
            return await _commentService.Delete(id);
        }

        [HttpPost]
        //{host}/api/comment
        public async Task<CommentView> Add([FromBody] Comment comment)
        {
            return await _commentService.Add(comment);
        }

        [HttpPut]
        //{host}/api/comment
        public async Task<CommentView> Edit(int id, [FromBody] Comment comment)
        {
            return await _commentService.Edit(id, comment);
        }
    }
}
