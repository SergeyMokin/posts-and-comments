using System.Collections.Generic;
using System.Threading.Tasks;
using Company.PostsAndComments.Interfaces;
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
    public class CommentController: Controller, ICommentController
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }


        [Route("{postId}")]
        public IEnumerable<CommentView> Get(int postId)
        {
            return _commentService.Get(postId);
        }

        [Route("{postId}/{id}")]
        public async Task<CommentView> Get(int postId, int id)
        {
            return await _commentService.Get(postId, id);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<CommentView> Delete(int id)
        {
            return await _commentService.Delete(id);
        }

        [HttpPost]
        public async Task<CommentView> Add([FromBody] Comment comment)
        {
            return await _commentService.Add(comment);
        }

        [HttpPut]
        public async Task<CommentView> Edit(int id, [FromBody] Comment comment)
        {
            return await _commentService.Edit(id, comment);
        }
    }
}
