using Blog_DevIO.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Blog_DevIO.Core.Services.Abstractions;
using Blog_DevIO.Core.ViewModels.Comments;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Blog_DevIO.API.Controllers
{
    [Authorize]
    [Route("api/posts/{postId:guid}/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IPostService _postService;

        public CommentController(IPostService postService)
        {
            _postService = postService;
        }

        [AllowAnonymous]
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<Comment>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetByPostId(Guid postId)
        {
            var Comment = await _postService.CommentService.GetByPostId(postId);
            if (Comment == null)
                return NotFound();

            return Ok(Comment);
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(Comment), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetById(Guid id, Guid postId)
        {
            var Comment = await _postService.CommentService.GetByPostIdAndId(postId, id);
            if (Comment == null)
                return NotFound();

            return Ok(Comment);
        }

        [HttpPost()]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post(ViewModel.CreateCommentViewModel comment, Guid postId)
        {
            if (ModelState.IsValid == false)
                return ValidationProblem(ModelState);

            CreateCommentViewModel commentViewModel = CreateCommentViewModel.Create(postId, comment.Content);

            await _postService.CommentService.Create(commentViewModel);

            return Created();
        }

        [HttpPut("{id:guid}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put(Guid id, Guid postId, EditCommentViewModel comment)
        {
            if (id != comment.Id)
                return BadRequest();

            if (postId != comment.PostId)
                return BadRequest();

            if (ModelState.IsValid == false)
                return ValidationProblem(ModelState);

            if (await _postService.CommentService.GetCommentToAction(postId, id) == null)
                return NotFound();

            try
            {
                await _postService.CommentService.Update(comment);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _postService.CommentService.GetById(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    await _postService.CommentService.Update(comment);
                }
            }

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(Guid id, Guid postId)
        {
            if (await _postService.CommentService.GetCommentToAction(postId, id) == null)
                return NotFound();

            await _postService.CommentService.Delete(postId, id);

            return NoContent();
        }
    }
}