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
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;

        public CourseController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Course> courses = await _context.Courses.Take(10).Skip(1).ToListAsync();
        
            return View(courses);
        }

        public async Task<IActionResult> Detail(int Id)
        {
            Course course = await _context.Courses.
                Where(m => m.Id == Id).
                Include(m=>m.Feature).
                FirstOrDefaultAsync();
        
            return View(course);
        }
    }
}
