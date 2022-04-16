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
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            List<Comment> comment = await _context.Comments.ToListAsync();
            CommentVM comments = new CommentVM
            {

                Comments = comment

            };
            return View(comments);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CommentVM commentVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }



            Comment comments = new Comment
            {
                Name = commentVM.Name,
                Email = commentVM.Email,
                Subject = commentVM.Subject,
                TextMessage = commentVM.TextMessage
            };

            await _context.Comments.AddAsync(comments);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            Comment comment = await _context.Comments.Where(m => m.Id == Id).FirstOrDefaultAsync();

            if (comment == null) return NotFound();


            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
