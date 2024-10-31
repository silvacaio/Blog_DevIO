using Blog_DevIO.Core.Services.Abstractions;
using Blog_DevIO.Core.ViewModels.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        // GET: Comments/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Comments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Comments/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
