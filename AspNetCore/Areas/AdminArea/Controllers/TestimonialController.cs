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
    public class TestimonialController : Controller
    {
        private readonly AppDbContext _context;
        public readonly IWebHostEnvironment _env;

        public TestimonialController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            List<Testimonial> testimonials = await _context.Testimonials.ToListAsync();
            return View(testimonials);
        }
        public IActionResult Detail(int Id)
        {
            var testimonial = _context.Testimonials.FirstOrDefault(m => m.Id == Id);
            return View(testimonial);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Testimonial testimonial)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();


            if (!testimonial.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View();
            }
            if (!testimonial.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "_" + testimonial.Photo.FileName;

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/testimonial", fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await testimonial.Photo.CopyToAsync(stream);
            }

            testimonial.Image = fileName;


            bool isExist = _context.Testimonials.Any(m => m.Name.ToLower().Trim() == testimonial.Name.ToLower().Trim());

            if (isExist)
            {
                ModelState.AddModelError("Name", "bu artiq movcuddur");
                return View();
            }


            await _context.Testimonials.AddAsync(testimonial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Testimonial testimonial = await GetTestimonialById(id);

            if (testimonial == null) return NotFound();

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/testimonial", testimonial.Image);

            Helper.DeleteFile(path);

            _context.Testimonials.Remove(testimonial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        private async Task<Testimonial> GetTestimonialById(int id)
        {
            return await _context.Testimonials.FindAsync(id);
        }

        public async Task<IActionResult> Update(int id)
        {
            var testimonial1 = await GetTestimonialById(id);
            if (testimonial1 == null) return NotFound();
            return View(testimonial1);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Testimonial testimonial)
        {
            var dbtestimonial = await GetTestimonialById(id);
            if (testimonial == null) return NotFound();
            if (dbtestimonial == null) return NotFound();
            if (id != testimonial.Id) return NotFound();

            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();


            if (!testimonial.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View(dbtestimonial);
            }
            if (!testimonial.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View(dbtestimonial);
            }


            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/testimonial", dbtestimonial.Image);

            Helper.DeleteFile(path);

            

            string fileName = Guid.NewGuid().ToString() + "_" + testimonial.Photo.FileName;

            string newPath = Helper.GetFilePath(_env.WebRootPath, "assets/img/testimonial", fileName);

            using (FileStream stream = new FileStream(newPath, FileMode.Create))
            {
                await testimonial.Photo.CopyToAsync(stream);
            }
   

            dbtestimonial.Image = fileName;
            dbtestimonial.Name = testimonial.Name;
            dbtestimonial.Description = testimonial.Description;
            dbtestimonial.Posinition = testimonial.Posinition;
          

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

     

        }

    }
}
