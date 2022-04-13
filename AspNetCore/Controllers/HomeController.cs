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
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _context.Sliders.ToListAsync();
          
            List<Service> services = await _context.Services.ToListAsync();
            About about = await _context.Abouts.FirstOrDefaultAsync();

            List<Course> courses=await _context.Courses.Include(m=>m.Feature).Take(4).Skip(1).ToListAsync();
            List<Notice> notices = await _context.Notices.ToListAsync();
        
            List<CourseFeatures> courseFeatures = await _context.CourseFeatures.ToListAsync();
            List<Testimonial> testimonials = await _context.Testimonials.ToListAsync();

            List<Blog> blogs = await _context.Blogs.Take(4).Skip(1).ToListAsync();

            List<Event> events = await _context.Events.ToListAsync();

            
            HomeVM homeVM = new HomeVM
            {
                Sliders=sliders,
                
                Services=services,
                About=about,
                Courses=courses,
                CourseFeatures=courseFeatures,
                Notices=notices,
                
                Testimonials=testimonials,
                Blogs=blogs,
                Events=events

                
            };

            

            return View(homeVM);
        }


        
    }
}
