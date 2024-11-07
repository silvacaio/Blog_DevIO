using Microsoft.AspNetCore.Mvc;
using Blog_DevIO.Core.ViewModels.Post;
using Blog_DevIO.Core.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;

namespace Blog_DevIO.Web.Controllers
{
    [Authorize]
    [Route("posts")]

    public class PostsController : Controller
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        // GET: Posts
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _postService.GetWithCommentsAndAuthorById());
        }

        // GET: Posts/user
        [HttpGet("user")]
        public async Task<IActionResult> User()
        {
            return View(await _postService.GetByUser());
        }

        // GET: Posts/Details/5
        [HttpGet("details/{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            var post = await _postService.GetPostWithCommentsAndAuthorById(id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePostViewModel post)
        {
            if (ModelState.IsValid == false)
                return View(post);

            await _postService.Create(post);

            return RedirectToAction(nameof(Index));

        }

        // GET: Posts/Edit/5
        [HttpGet("edit/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var post = await _postService.GetById(id);

            if (post == null)
                return NotFound();

            if (post.CanEdit == false)
                return RedirectToAction("Index", "Error", new { statusCode = 403 });


            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.        
        [HttpPost("edit/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PostViewModel postViewModel)
        {
            if (id != postViewModel.Id)
                return NotFound();

            if (ModelState.IsValid == false)
                return View(postViewModel);

            var post = await _postService.GetById(postViewModel.Id);
            if (post == null)
                return NotFound();

            if (post.CanEdit == false)
                return RedirectToAction("Index", "Error", new { statusCode = 403 });

            await _postService.Update(postViewModel);

            return RedirectToAction("Details", new { id = postViewModel.Id });
        }

        // GET: Posts/Delete/5
        [Authorize, HttpGet("delete/{id:guid}")]

        public async Task<IActionResult> Delete(Guid id)
        {
            var post = await _postService.GetById(id);

            if (post == null)
                return NotFound();

            if (post.CanEdit == false)
                return RedirectToAction("Index", "Error", new { statusCode = 403 });

            return View(post);
        }

        // POST: Posts/Delete/5
        [Authorize, HttpPost("delete/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var post = await _postService.GetById(id);

            if (post == null)
                return NotFound();

            if (post.CanEdit == false)
                return RedirectToAction("Index", "Error", new { statusCode = 403 });

            await _postService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
