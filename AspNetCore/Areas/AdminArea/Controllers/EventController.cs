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
    public class EventController : Controller
    {

        private readonly AppDbContext _context;
        public readonly IWebHostEnvironment _env;
        public EventController(AppDbContext context, IWebHostEnvironment env)
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
            if (!events.Photo.CheckFileSize(2000))
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


            bool isExist = _context.Events.Any(m => m.Header.ToLower().Trim() == events.Header.ToLower().Trim());

            if (isExist)
            {
                ModelState.AddModelError("Header", "bu artiq movcuddur");
                return View();
            }


            await _context.Events.AddAsync(events);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Event events = await GetEventById(id);

            if (events == null) return NotFound();

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/event", events.Image);

            Helper.DeleteFile(path);

            _context.Events.Remove(events);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        private async Task<Event> GetEventById(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task<IActionResult> Update(int id)
        {
            var events = await GetEventById(id);
            if (events == null) return NotFound();
            return View(events);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Event events)
        {
            var dbevents = await GetEventById(id);
            if (events == null) return NotFound();
            if (dbevents == null) return NotFound();
            if (id != events.Id) return NotFound();

            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();


            if (!events.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View(events);
            }
            if (!events.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View(events);
            }


            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/event", dbevents.Image);

            Helper.DeleteFile(path);



            string fileName = Guid.NewGuid().ToString() + "_" + events.Photo.FileName;

            string newPath = Helper.GetFilePath(_env.WebRootPath, "assets/img/event", fileName);

            using (FileStream stream = new FileStream(newPath, FileMode.Create))
            {
                await events.Photo.CopyToAsync(stream);
            }


            dbevents.Image = fileName;
            dbevents.Time = events.Time;
            dbevents.Header = events.Header;
            dbevents.DayTime = events.DayTime;
            dbevents.Location = events.Location;





            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));



        }

        public IActionResult Detail(int Id)
        {
            var events = _context.Events.FirstOrDefault(m => m.Id == Id);
            return View(events);
        }
    }
}
