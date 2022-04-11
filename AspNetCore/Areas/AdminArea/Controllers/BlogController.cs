using AspNetCore.Data;
using AspNetCore.Models;
using AspNetCore.Utilities.File;
using AspNetCore.Utilities.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        public readonly IWebHostEnvironment _env;
        public BlogController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }


        public async Task<IActionResult> Index()
        {
            List<Blog> blogs = await _context.Blogs.ToListAsync();
            return View(blogs);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Detail(int Id)
        {
            var blog = _context.Blogs.FirstOrDefault(m => m.Id == Id);
            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();


            if (!blog.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View();
            }
            if (!blog.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "_" + blog.Photo.FileName;

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/blog", fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await blog.Photo.CopyToAsync(stream);
            }

            blog.Image = fileName;


            bool isExist = _context.Blogs.Any(m => m.Description.ToLower().Trim() == blog.Description.ToLower().Trim());

            if (isExist)
            {
                ModelState.AddModelError("Description", "bu artiq movcuddur");
                return View();
            }


            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

         [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Blog blog = await GetBlogById(id);

            if (blog == null) return NotFound();

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/blog", blog.Image);

            Helper.DeleteFile(path);

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        private async Task<Blog> GetBlogById(int id)
        {
            return await _context.Blogs.FindAsync(id);
        }

        public async Task<IActionResult> Update(int id)
        {
            var blog = await GetBlogById(id);
            if (blog == null) return NotFound();
            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Blog blog)
        {
            var dbblog = await GetBlogById(id);
            if (blog == null) return NotFound();
            if (dbblog == null) return NotFound();
            if (id != blog.Id) return NotFound();

            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();


            if (!blog.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View(blog);
            }
            if (!blog.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View(blog);
            }


            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/testimonial", dbblog.Image);

            Helper.DeleteFile(path);



            string fileName = Guid.NewGuid().ToString() + "_" + blog.Photo.FileName;

            string newPath = Helper.GetFilePath(_env.WebRootPath, "assets/img/blog", fileName);

            using (FileStream stream = new FileStream(newPath, FileMode.Create))
            {
                await blog.Photo.CopyToAsync(stream);
            }


            dbblog.Image = fileName;
            dbblog.Time = blog.Time;
            dbblog.Description = blog.Description;
            


            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));



        }
    }
}
