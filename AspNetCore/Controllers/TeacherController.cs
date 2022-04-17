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
    public class TeacherController : Controller
    {

        private readonly AppDbContext _context;

        public TeacherController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Teacher> teachers = await _context.Teachers.Take(13).Skip(1).ToListAsync();
            return View(teachers);
        }

        public async Task<IActionResult> Detail(int Id)
        {
            Teacher teacher = await _context.Teachers.Where(m => m.Id == Id).FirstOrDefaultAsync();
            return View(teacher);
        }
    }
}
