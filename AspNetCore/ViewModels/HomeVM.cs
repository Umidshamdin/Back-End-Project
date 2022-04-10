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
        public SliderDetail SliderDetail { get; set; }

        public List<Service> Services { get; set; }

        public About About { get; set; }


        public List<Notice> Notices { get; set; }

        public List<Event> Events { get; set; }

        public List<EventDetail> EventDetails { get; set; }

        public List<Testimonial> Testimonials { get; set; }





    }
}
