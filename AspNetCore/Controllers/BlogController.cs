using AspNetCore.Data;
using AspNetCore.Models;
using AspNetCore.Utilities.Pagination;
using AspNetCore.ViewModels;
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
        public async Task<IActionResult> Index(int page=1,int take=3)
        {
            var blogs = await _context.Blogs
                
                .Skip((page-1)*take)
                .Take(take)
                .AsNoTracking()
                .ToListAsync();
            var blogsVM = GetMapDatas(blogs);

            int count = await GetPageCount(take);

            Paginate<BlogListVM> result = new Paginate<BlogListVM>(blogsVM, page, count);




            return View(result);
         
        }

        private List<BlogListVM> GetMapDatas(List<Blog> blogs)
        {
            List<BlogListVM> blogLists = new List<BlogListVM>();

            foreach (var blog in blogs)
            {
                BlogListVM newBlog = new BlogListVM
                {
                    Id = blog.Id,
                    Author=blog.Author,
                    Description=blog.Description,
                    Time=blog.Time,
                    BlogName = blog.BlogName,
                    Image = blog.Image
                };
                blogLists.Add(newBlog);

            }
            return blogLists;
        }
        private async Task<int> GetPageCount(int take)
        {
            var count = await _context.Blogs.CountAsync();
            return (int)Math.Ceiling((decimal)count / take);
        }

        public async Task<IActionResult> Detail(int Id)
        {
            Blog blogs = await _context.Blogs.Where(m=>m.Id==Id).FirstOrDefaultAsync();
            return View(blogs);
        }
    }
}
