using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return (await Task.FromResult(View()));
        }
    }
}

//private readonly AppDbContext _context;
//public NoticeViewComponent(AppDbContext context)
//{
//    _context = context;
//}
//public async Task<IViewComponentResult> InvokeAsync()
//{
//    List<Notice> notices = await _context.Notices.ToListAsync();
//    return (await Task.FromResult(View(notices)));
//}
//    }