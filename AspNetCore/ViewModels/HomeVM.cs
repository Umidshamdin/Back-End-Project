using AspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<Service> Services { get; set; }
        public About About { get; set; }
        public List<Notice> Notices { get; set; }
        public List<Testimonial> Testimonials { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Course> Courses { get; set; }
        public List<CourseFeatures> CourseFeatures { get; set; }
        public List<Event> Events { get; set; }
        public Subscripe Subscripe { get; set; }
    }
}
