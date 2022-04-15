using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.ViewModels
{
    public class BlogListVM
    {
        public int Id { get; set; }

        public string Image { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string BlogName { get; set; }
        public DateTime Time { get; set; }

    }
}
