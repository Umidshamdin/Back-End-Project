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
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Blog> blogs = await _context.Blogs.Take(10).Skip(1).ToListAsync();
            return View(blogs);
        }

        public async Task<IActionResult> Detail(int Id)
        {
            Blog blogs = await _context.Blogs.Where(m=>m.Id==Id).FirstOrDefaultAsync();
            return View(blogs);
        }
    }
}
