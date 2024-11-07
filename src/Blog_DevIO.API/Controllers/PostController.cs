using Blog_DevIO.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Blog_DevIO.Core.Services.Abstractions;
using Blog_DevIO.Core.ViewModels.Post;
using Microsoft.EntityFrameworkCore;

namespace Blog_DevIO.API.Controllers
{
    [Authorize]
    [Route("api/")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IAppUserService _userService;
        public PostController(IPostService postService, IAppUserService userService)
        {
            _postService = postService;
            _userService = userService;
        }

        [HttpGet("post")]
        [ProducesResponseType(typeof(IEnumerable<Post?>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _postService.Get());
        }

        [HttpGet("post/{id:guid}")]
        [ProducesResponseType(typeof(Post), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetById(Guid id)
        {
            var post = await _postService.GetById(id);
            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpGet("post/getbyuser")]
        [ProducesResponseType(typeof(Post), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetByUser()
        {
            var post = await _postService.GetByUser();
            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPost("post")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(Post), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post(CreatePostViewModel post)
        {
            if (ModelState.IsValid == false)
                return ValidationProblem(ModelState);

            await _postService.Create(post);

            return RedirectToAction("GetByUser");
        }

        [HttpPut("post")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(Post), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(Guid id, EditPostViewModel post)
        {
            if (id != post.Id)
                return BadRequest();

            if (ModelState.IsValid == false)
                return ValidationProblem(ModelState);

            if (await _postService.GetPostToAction(id) == null)
                return NotFound();

            try
            {
                await _postService.Update((PostViewModel)post);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _postService.GetById(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            await _postService.Update((PostViewModel)post);

            return RedirectToAction("GetByUser");
        }


        [Authorize]
        [HttpDelete("post")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (await _postService.GetPostToAction(id) == null)
                return NotFound();

            await _postService.Delete(id);

            return NoContent();
        }
    }
}