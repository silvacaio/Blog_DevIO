﻿using Microsoft.AspNetCore.Mvc;
using Blog_DevIO.Core.ViewModels.Post;
using Blog_DevIO.Core.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;

namespace Blog_DevIO.Web.Controllers
{
    [Route("posts")]

    public class PostsController : Controller
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            return View(await _postService.Get());
        }

        // GET: Posts/Details/5
        [HttpGet("details/{id:guid}")]

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
        [HttpPost]
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
            {
                return NotFound();
            }

            if (post.CanEdit == false)
                return RedirectToAction("Index", "Error", new { statusCode = 403 });


            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EditPostViewModel postViewModel)
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

            return RedirectToAction("Index");
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
