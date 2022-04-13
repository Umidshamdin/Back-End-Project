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
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        public readonly IWebHostEnvironment _env;

        public CourseController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            List<Course> courses = await _context.Courses.Include(m=>m.Feature).ToListAsync();
            return View(courses);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();


            if (!course.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View();
            }
            if (!course.Photo.CheckFileSize(2000))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "_" + course.Photo.FileName;

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/course", fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await course.Photo.CopyToAsync(stream);
            }
            List<Course> courses = await _context.Courses.ToListAsync();

            course.Image = fileName;
            
            



            bool isExist = _context.Courses.Any(m => m.Description.ToLower().Trim() == course.Description.ToLower().Trim());

            if (isExist)
            {
                ModelState.AddModelError("Description", "bu artiq movcuddur");
                return View();
            }


            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Course course = await GetCourseId(id);

            if (course == null) return NotFound();

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/course", course.Image);

            Helper.DeleteFile(path);

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        private async Task<Course> GetCourseId(int id)
        {
            return await _context.Courses.FindAsync(id);
        }
        public async Task<IActionResult> Update(int id)
        {
            var course = await GetCourseId(id);
            if (course == null) return NotFound();
            return View(course);
        }

        public IActionResult Detail(int Id)
        {
            var course = _context.Courses.FirstOrDefault(m => m.Id == Id);
            return View(course);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Course course)
        {
            var dbcourse = await GetCourseId(id);
            if (course == null) return NotFound();

            if (dbcourse == null) return NotFound();
            if (id != course.Id) return NotFound();


            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();


            if (!course.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View(dbcourse);
            }
            if (!course.Photo.CheckFileSize(2000))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View(dbcourse);
            }


            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/course", dbcourse.Image);

            Helper.DeleteFile(path);



            string fileName = Guid.NewGuid().ToString() + "_" + course.Photo.FileName;

            string newPath = Helper.GetFilePath(_env.WebRootPath, "assets/img/course", fileName);

            using (FileStream stream = new FileStream(newPath, FileMode.Create))
            {
                await course.Photo.CopyToAsync(stream);
            }
            dbcourse.Image = fileName;
            dbcourse.Name = course.Name;
            dbcourse.Description = course.Description;

            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }

    }
}
