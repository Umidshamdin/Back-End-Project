using AspNetCore.Data;
using AspNetCore.Models;
using AspNetCore.Utilities.File;
using AspNetCore.Utilities.Helpers;
using AspNetCore.ViewModels;
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
    public class EventController : Controller
    {
        private readonly AppDbContext _context;
        public readonly IWebHostEnvironment _env;
        public EventController(AppDbContext context,  IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
           
        }
        public async Task<IActionResult> Index()
        {
            List<Event> events = await _context.Events.ToListAsync();
            return View(events);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event events)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();


            if (!events.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View();
            }
            if (!events.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "_" + events.Photo.FileName;

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/event", fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await events.Photo.CopyToAsync(stream);
            }
           
            events.Image = fileName;

            

            await _context.Events.AddAsync(events);
            await _context.SaveChangesAsync();


            
            return RedirectToAction(nameof(Index));
        }
    }
}
