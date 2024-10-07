using Blog_DevIO.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Blog_DevIO.Application.Services.Abstractions;
using Blog_DevIO.Application.ViewModels.Post;
using Blog_DevIO.API.Configurations;


namespace Blog_DevIO.API.Controllers
{
    [Route("api/")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly JwtSettings _jwtSettings;


        public PostController(IPostService postService)
        {
            _postService = postService;
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
            var post = await _postService.GetByUser("");
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

            await _postService.Create(post, "");

            return RedirectToAction("GetByUser");
        }

        [HttpPut("post")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(Post), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(EditPostViewModel post)
        {
            if (ModelState.IsValid == false)
                return ValidationProblem(ModelState);

            await _postService.Update(post, "");

            return RedirectToAction("GetByUser");
        }


        [HttpDelete("post")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var post = await _postService.GetById(id);
            if (post == null)
                return NotFound();

            await _postService.Delete(id);

            return NoContent();
        }
    }
}