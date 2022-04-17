using AspNetCore.Data;
using AspNetCore.Models;
using AspNetCore.Utilities.File;
using AspNetCore.Utilities.Helpers;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin")]

    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;
        public readonly IWebHostEnvironment _env;

        public TeacherController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            List<Teacher> teachers = await _context.Teachers.ToListAsync();
            return View(teachers);
        }

        public IActionResult Detail(int Id)
        {
            var teacher = _context.Teachers.FirstOrDefault(m => m.Id == Id);
            return View(teacher);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Teacher teacher)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();


            if (!teacher.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View();
            }
            if (!teacher.Photo.CheckFileSize(2000))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "_" + teacher.Photo.FileName;

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/teacher", fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await teacher.Photo.CopyToAsync(stream);
            }

            teacher.Image = fileName;


            bool isExist = _context.Teachers.Any(m => m.Name.ToLower().Trim() == teacher.Name.ToLower().Trim());

            if (isExist)
            {
                ModelState.AddModelError("Name", "bu artiq movcuddur");
                return View();
            }


            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Teacher teacher = await GetTeacherById(id);

            if (teacher == null) return NotFound();

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/teacher", teacher.Image);

            Helper.DeleteFile(path);

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        private async Task<Teacher> GetTeacherById(int id)
        {
            return await _context.Teachers.FindAsync(id);
        }

        public async Task<IActionResult> Update(int id)
        {
            var teacher = await GetTeacherById(id);
            if (teacher == null) return NotFound();
            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Teacher teacher)
        {
            var dbteacher = await GetTeacherById(id);
            if (teacher == null) return NotFound();
            if (dbteacher == null) return NotFound();
            if (id != teacher.Id) return NotFound();

            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();


            if (!teacher.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View(teacher);
            }
            if (!teacher.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View(teacher);
            }


            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/teacher", dbteacher.Image);

            Helper.DeleteFile(path);



            string fileName = Guid.NewGuid().ToString() + "_" + teacher.Photo.FileName;

            string newPath = Helper.GetFilePath(_env.WebRootPath, "assets/img/teacher", fileName);

            using (FileStream stream = new FileStream(newPath, FileMode.Create))
            {
                await teacher.Photo.CopyToAsync(stream);
            }


            dbteacher.Image = fileName;
            dbteacher.Name = teacher.Name;
            dbteacher.Posinition = teacher.Posinition;
            dbteacher.About = teacher.About;
            dbteacher.Experience = teacher.Experience;
            dbteacher.Hobby = teacher.Hobby;
            dbteacher.Faculty = teacher.Faculty;
            dbteacher.PhoneNumber = teacher.PhoneNumber;
            dbteacher.Email = teacher.Email;
            dbteacher.SkillName = teacher.SkillName;







            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));



        }
    }
}
