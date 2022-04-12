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
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        public readonly IWebHostEnvironment _env;


        public AboutController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Update()
        {
            About about = await _context.Abouts.FirstOrDefaultAsync();
            if (about == null) return NotFound();
            return View(about);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update( About about)
        {
            var dbabout = await _context.Abouts.FirstOrDefaultAsync();
           
            if (dbabout == null) return NotFound();
            

            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();


            if (!about.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View(dbabout);
            }
            if (!about.Photo.CheckFileSize(2000))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View(dbabout);
            }


            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/about", dbabout.Image);

            Helper.DeleteFile(path);



            string fileName = Guid.NewGuid().ToString() + "_" + about.Photo.FileName;

            string newPath = Helper.GetFilePath(_env.WebRootPath, "assets/img/about", fileName);

            using (FileStream stream = new FileStream(newPath, FileMode.Create))
            {
                await about.Photo.CopyToAsync(stream);
            }


            dbabout.Image = fileName;
            dbabout.Header = about.Header;
            dbabout.Description = about.Description;
         


            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Update));



        }

        

    }
}
