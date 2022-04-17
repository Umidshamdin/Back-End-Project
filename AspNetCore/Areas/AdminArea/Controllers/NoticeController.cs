using System;
using AspNetCore.Data;
using AspNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCore.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "Admin")]

    public class NoticeController : Controller
    {

        private readonly AppDbContext _context;

        public NoticeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {

            List<Notice> notices = await _context.Notices.ToListAsync();
            return View(notices);
        }

        public IActionResult Detail(int Id)
        {
            var notice = _context.Notices.FirstOrDefault(m => m.Id == Id);
            return View(notice);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Notice notice)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool isExist = _context.Notices.Any(m => m.Description.ToLower().Trim() == notice.Description.ToLower().Trim());

            if (isExist)
            {
                ModelState.AddModelError("Name", "bu artiq movcuddur");
                return View();
            }

            notice.Time = DateTime.Now.ToLongDateString();
            await _context.Notices.AddAsync(notice);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            Notice notice = await _context.Notices.Where(m => m.Id == id).FirstOrDefaultAsync();
            if (notice == null) return NotFound();
            return View(notice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Notice notice)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id != notice.Id) return NotFound();

            Notice dbnotice = await _context.Notices.AsNoTracking().Where(m => m.Id == id).FirstOrDefaultAsync();
           

            bool isExist = _context.Notices.Any(m => m.Description.ToLower().Trim() == notice.Description.ToLower().Trim() && m.Description.ToLower().Trim() == notice.Description.ToLower().Trim());
            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu servise movcud");
                return View();
            }

            _context.Update(notice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int id)
        {
            Notice notice = await _context.Notices.Where(m => m.Id == id).FirstOrDefaultAsync();
            if (notice == null) return NotFound();

            _context.Notices.Remove(notice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
