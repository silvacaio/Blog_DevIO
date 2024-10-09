using Blog_DevIO.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Blog_DevIO.Application.Services.Abstractions;
using Blog_DevIO.Application.ViewModels.Comments;
using Blog_DevIO.Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Blog_DevIO.API.Controllers
{
    [Route("api/")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }


        [HttpGet("comment")]
        [ProducesResponseType(typeof(IEnumerable<Comment?>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _commentService.Get());
        }


        [HttpGet("comment/{id:guid}")]
        [ProducesResponseType(typeof(Comment), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetById(Guid id)
        {
            var Comment = await _commentService.GetById(id);
            if (Comment == null)
                return NotFound();

            return Ok(Comment);
        }

        [HttpPost("comment")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(Post), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post(CreateCommentViewModel comment)
        {
            if (ModelState.IsValid == false)
                return ValidationProblem(ModelState);

            await _commentService.Create(comment);

            return RedirectToAction("GetByUser");
        }

        [HttpPut("comment")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(Post), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(Guid id, EditCommentViewModel comment)
        {
            if (id != comment.Id)
                return BadRequest();

            if (ModelState.IsValid == false)
                return ValidationProblem(ModelState);

            if (_commentService.GetCommentToAction(id) == null)
                return NotFound();

            try
            {
                await _commentService.Update(comment);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_commentService.GetById(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            await _commentService.Update(comment);

            return RedirectToAction("GetByUser");
        }


        [HttpDelete("comment")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (_commentService.GetCommentToAction(id) == null)
                return NotFound();

            await _commentService.Delete(id);

            return NoContent();
        }
    }
}