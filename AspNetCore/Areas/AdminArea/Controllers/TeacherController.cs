using AspNetCore.Data;
using AspNetCore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]

    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;
        public readonly IWebHostEnvironment _env;

        public TeacherController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            List<Teacher> teachers = await _context.Teachers.ToListAsync();
            return View(teachers);
        }
    }
}
