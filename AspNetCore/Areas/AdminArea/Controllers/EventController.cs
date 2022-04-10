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
        public async Task<IActionResult> Create(EventVM eventVM)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();


            if (!eventVM.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View();
            }
            if (!eventVM.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "_" + eventVM.Photo.FileName;

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/event", fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await eventVM.Photo.CopyToAsync(stream);
            }

            EventDetail eventDetail = new EventDetail
            {
                
                DetailImage = fileName,
                
                Name = eventVM.Name,
                Description = eventVM.Description

            };

           
          



            await _context.EventDetails.AddAsync(eventDetail);
            await _context.SaveChangesAsync();

            if (!eventVM.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View();
            }
            if (!eventVM.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View();
            }

            fileName = Guid.NewGuid().ToString() + "_" + eventVM.Photo.FileName;

            path = Helper.GetFilePath(_env.WebRootPath, "assets/img/event", fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await eventVM.Photo.CopyToAsync(stream);
            }

            List<EventDetail> eventDetails = await _context.EventDetails.ToListAsync();

            Event @event = new Event
            {
                Image=fileName,
                Time = (DateTime) eventVM.Time,
                Header=eventVM.Header,
                DayTime=eventVM.DayTime,
                Address=eventVM.Address,
               
                EventDetailId=eventDetails.LastOrDefault().Id
            };

            await _context.Events.AddAsync(@event);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detail(int Id)
        {
            var even = _context.Events.FirstOrDefault(m => m.Id == Id);
            return View(even);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Event events = await GetSliderById(id);

            if (events == null) return NotFound();

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/event", events.Image);

            Helper.DeleteFile(path);

            _context.Events.Remove(events);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        private async Task<Event> GetSliderById(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        #region Edit
        public async Task<IActionResult> Update(int Id)
        {
            var @event = await GetEventById(Id);
            EventVM eventVM = new EventVM
            {
                Time = @event.Time,
                Header=@event.Header,
                DayTime = @event.DayTime,
                Address = @event.Address,
                Name=@event.EventDetail.Name,
                Description = @event.EventDetail.Description,
                Image = @event.Image,
                DetailImage = @event.EventDetail.DetailImage
            };
            if (eventVM == null) return NotFound();
            return View(eventVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int Id, EventVM eventVM)
        {
            var dbEvents = await GetEventById(Id);
            if (dbEvents == null) return NotFound();

            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();

            if (!eventVM.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View(dbEvents);
            }

            if (!eventVM.Photo.CheckFileSize(800))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View(dbEvents);
            }
            if (ModelState["DetailPhoto"].ValidationState == ModelValidationState.Invalid) return View();

            if (!eventVM.DetailPhoto.CheckFileType("image/"))
            {
                ModelState.AddModelError("DetailPhoto", "Image type is wrong");
                return View(dbEvents);
            }

            if (!eventVM.DetailPhoto.CheckFileSize(800))
            {
                ModelState.AddModelError("DetailPhoto", "Image size is wrong");
                return View(dbEvents);
            }

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/event", dbEvents.Image);
            Helper.DeleteFile(path);

            string pathDetail = Helper.GetFilePath(_env.WebRootPath, "assets/img/event", dbEvents.EventDetail.DetailImage);
            Helper.DeleteFile(pathDetail);

            string fileName = Guid.NewGuid().ToString() + "_" + eventVM.Photo.FileName;
            path = Helper.GetFilePath(_env.WebRootPath, "assets/img/event", fileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await eventVM.Photo.CopyToAsync(stream);
            }
            dbEvents.Image = fileName;
            fileName = Guid.NewGuid().ToString() + "_" + eventVM.DetailPhoto.FileName;
            path = Helper.GetFilePath(_env.WebRootPath, "assets/img/event", fileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await eventVM.DetailPhoto.CopyToAsync(stream);
            }
            dbEvents.EventDetail.DetailImage = fileName;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Helper
        private async Task<Event> GetEventById(int Id)
        {
            return await _context.Events.Where(m => m.Id == Id).Include(m => m.EventDetail).FirstOrDefaultAsync();
        }
        #endregion





    }
}
