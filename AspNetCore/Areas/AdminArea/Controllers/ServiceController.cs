using AspNetCore.Data;
using AspNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;
        
        public ServiceController(AppDbContext context)
        {
            _context = context;
            
        }
        public async Task<IActionResult> Index()
        {
            List<Service> services = await _context.Services.AsNoTracking().ToListAsync();
            return View(services);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Service service)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool isExist = _context.Services.Any(m => m.Description.ToLower().Trim() == service.Description.ToLower().Trim());

            if (isExist)
            {
                ModelState.AddModelError("Name", "bu artiq movcuddur");
                return View();
            }
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            Service service = await _context.Services.Where(m => m.Id == id).FirstOrDefaultAsync();
            if (service == null) return NotFound();
            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id,Service service)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id != service.Id) return NotFound();

            Service dbService = await _context.Services.AsNoTracking().Where(m => m.Id == id).FirstOrDefaultAsync();
            if(dbService.Header.ToLower().Trim()==service.Header.ToLower().Trim() && dbService.Description.ToLower().Trim() == service.Description.ToLower().Trim())
            {
                return RedirectToAction(nameof(Index));
            }

            bool isExist = _context.Services.Any(m => m.Header.ToLower().Trim() == service.Header.ToLower().Trim() && m.Description.ToLower().Trim() == service.Description.ToLower().Trim());
            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu servise movcud");
                return View();
            }

            _context.Update(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(int id)
        {
            Service service = await _context.Services.Where(m => m.Id == id).FirstOrDefaultAsync();
            if (service == null) return NotFound();

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
