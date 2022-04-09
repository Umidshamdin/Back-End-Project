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
            SliderDetail sliderDetail = await _context.SliderDetails.FirstOrDefaultAsync();
            List<Service> services = await _context.Services.ToListAsync();
            HomeVM homeVM = new HomeVM
            {
                Sliders=sliders,
                SliderDetail=sliderDetail,
                Services=services
            };
            return View(homeVM);
        }
    }
}
