using AspNetCore.Data;
using AspNetCore.Models;
using AspNetCore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Controllers
{
    public class AboutController : Controller
    {

        private readonly AppDbContext _context;

        public AboutController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            About about = await _context.Abouts.FirstOrDefaultAsync();
            List<Teacher> teachers = await _context.Teachers.Take(5).Skip(1).ToListAsync();
            List<Testimonial> testimonials = await _context.Testimonials.ToListAsync();
            List<Notice> notices = await _context.Notices.ToListAsync();
            Subscripe subscripe = await _context.Subscripes.FirstOrDefaultAsync();



            AboutVM aboutVM = new AboutVM
            {
                About=about,
                Teachers=teachers,
                Testimonials=testimonials,
                Notices=notices,
                Subscripe=subscripe
            };

            return View(aboutVM);
        }
    }
}
