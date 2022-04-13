using AspNetCore.Data;
using AspNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Controllers
{
    public class EventController : Controller
    {
        private readonly AppDbContext _context;

        public EventController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Event> events = await _context.Events.Take(10).Skip(1).ToListAsync();
            return View(events);
        }

        public async Task<IActionResult> Detail(int Id)
        {
            Event events = await _context.Events.Where(m => m.Id == Id).FirstOrDefaultAsync();
            return View(events);
        }
    }
}
