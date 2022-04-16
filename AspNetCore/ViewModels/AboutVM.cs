using AspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.ViewModels
{
    public class AboutVM
    {
        public About About { get; set; }

        public List<Teacher> Teachers { get; set; }

        public List<Testimonial> Testimonials { get; set; }

        public List<Notice> Notices { get; set; }
        public Subscripe Subscripe { get; set; }



    }
}
