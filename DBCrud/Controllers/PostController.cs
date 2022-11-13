using DBCrud.Helpers;
using DBCrud.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DBCrud.Controllers
{
    public class PostController : Controller
    {
        private readonly BlogDbContext blogDbContext;

        public PostController(BlogDbContext blogDbContext)
        {
            this.blogDbContext = blogDbContext;
        }


        [HttpGet]
        public IActionResult Add()
        {
            //ModelState.AddModelError("Title", "99999");
            return View();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(blogDbContext.Posts.ToList());
        }

        [HttpPost]
        //  [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Post post, IFormFile ImageUrl)
        {
            try
            {
                post.ImageUrl = await FileUploadHelper.Upload(ImageUrl);
            }
            catch (Exception) { }


            if (ModelState.IsValid)
            {
                post.Date = DateTime.Now;

                blogDbContext.Posts.AddAsync(post);
                await blogDbContext.SaveChangesAsync();
                TempData["Status"] = "New post added!";
                return RedirectToAction("Index");
            }

            return View(post);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Post post = blogDbContext.Posts.Find(id);
            if (post != null)
            {
                blogDbContext.Posts.Remove(post);
                await blogDbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Post post = blogDbContext.Posts.Find(id);
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Post post, IFormFile ImageUrl)
        {
            try
            {
                post.ImageUrl = await FileUploadHelper.Upload(ImageUrl);
            }
            catch (Exception) { }
            Post postDb = blogDbContext.Posts.Find(post.Id);
            if (postDb != null)
            {
                postDb.Title = post.Title;
                postDb.ImageUrl = post.ImageUrl;
                postDb.Content = post.Content;
                postDb.Date = post.Date;
                await blogDbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

    }
}
