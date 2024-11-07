using Blog_DevIO.Core.Services.Abstractions;
using Blog_DevIO.Core.ViewModels.Comments;
using Blog_DevIO.Core.ViewModels.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog_DevIO.Web.Controllers
{
    [Authorize]
    [Route("comments")]
    public class CommentsController : Controller
    {
        private readonly IPostService _postService;

        public CommentsController(IPostService postService)
        {
            _postService = postService;
        }

        // POST: Comments/Create
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCommentViewModel comentarioViewModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_NewCommentPartial", comentarioViewModel);
            }

            var postId = comentarioViewModel.PostId;
            var post = await _postService.GetById(postId);

            if (post == null) return NotFound();

            await _postService.CommentService.Create(comentarioViewModel);

            var comments = await _postService.CommentService.GetByPostId(postId);
            return PartialView("_ListCommentsPartial", comments);
        }

        // GET: Comments/Edit/5/5
        [HttpGet("edit/{id:guid}/{postId:guid}")]
        public async Task<IActionResult> Edit(Guid id, Guid postId)
        {
            var comment = await _postService.CommentService.GetByPostIdAndId(postId, id);

            if (comment == null)
                return NotFound();

            if (comment.CanEdit == false)
                return RedirectToAction("Index", "Error", new { statusCode = 403 });


            return PartialView("_EditCommentPartial", comment); // Return the partial view
        }


        // POST: Posts/Edit/5

        [HttpPost("edit/{id:guid}/{postId:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Guid postId, CommentViewModel commentViewModel)
        {
            if (id != commentViewModel.Id)
                return NotFound();

            if (postId != commentViewModel.PostId)
                return NotFound();

            ModelState.Remove(nameof(commentViewModel.Author));

            if (ModelState.IsValid == false)
                return View(commentViewModel);

            var comment = await _postService.CommentService.GetByPostIdAndId(postId, id);
            if (comment == null)
                return NotFound();

            if (comment.CanEdit == false)
                return RedirectToAction("Index", "Error", new { statusCode = 403 });

            await _postService.CommentService.Update(commentViewModel);

            var comments = await _postService.CommentService.GetByPostId(postId);
            return PartialView("_ListCommentsPartial", comments);
        }


        // POST: Comments/Delete/5
        [HttpPost("delete/{id:guid}/{postId:guid}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id, Guid postId)
        {
            var comment = await _postService.CommentService.GetByPostIdAndId(postId, id);
            if (comment == null)
                return NotFound();

            if (comment.CanEdit == false)
                return Forbid();

            await _postService.CommentService.Delete(postId, id);

            var comments = await _postService.CommentService.GetByPostId(postId);
            return PartialView("_ListCommentsPartial", comments);
        }
    }
}
