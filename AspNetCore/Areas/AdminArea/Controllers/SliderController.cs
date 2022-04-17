using AspNetCore.Data;
using AspNetCore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using AspNetCore.Utilities.File;
using AspNetCore.Utilities.Helpers;

namespace AspNetCore.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        public readonly IWebHostEnvironment _env;
        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _context.Sliders.AsNoTracking().ToListAsync();
            return View(sliders);
        }
        public IActionResult Detail(int Id)
        {
            var slider = _context.Sliders.FirstOrDefault(m => m.Id == Id);
            return View(slider);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();


            if (!slider.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View();
            }
            if (!slider.Photo.CheckFileSize(2000))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "_" + slider.Photo.FileName;

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/slider", fileName);

            using(FileStream stream = new FileStream(path, FileMode.Create))
            {
                await slider.Photo.CopyToAsync(stream);
            }

            slider.Image = fileName;


            bool isExist = _context.Sliders.Any(m => m.Header.ToLower().Trim() == slider.Header.ToLower().Trim());

            if (isExist)
            {
                ModelState.AddModelError("Header", "bu artiq movcuddur");
                return View();
            }


            await _context.Sliders.AddAsync(slider);

            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Slider slider = await GetSliderById(id);

            if (slider == null) return NotFound();

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/slider", slider.Image);

            Helper.DeleteFile(path);

            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            var slider = await GetSliderById(id);
            if (slider == null) return NotFound();
            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id,Slider slider)
        {
            var dbslider = await GetSliderById(id);
            if (slider == null) return NotFound();

            if (dbslider == null) return NotFound();
            if (id != slider.Id) return NotFound();


            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();


            if (!slider.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View(dbslider);
            }
            if (!slider.Photo.CheckFileSize(2000))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View(dbslider);
            }
         

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/slider", dbslider.Image);

            Helper.DeleteFile(path);

          

            string fileName = Guid.NewGuid().ToString() + "_" + slider.Photo.FileName;

            string newPath = Helper.GetFilePath(_env.WebRootPath, "assets/img/slider",fileName);

            using (FileStream stream = new FileStream(newPath, FileMode.Create))
            {
                await slider.Photo.CopyToAsync(stream);
            }
            dbslider.Image = fileName;
            dbslider.Header = slider.Header;
            dbslider.Description = slider.Description;
           
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }
        private async Task<Slider> GetSliderById( int id)
        {
            return await _context.Sliders.FindAsync(id);
        }
    }
}
