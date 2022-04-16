using AspNetCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Data
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        public DbSet<Slider> Sliders { get; set; }
       

        public DbSet<Service> Services { get; set; }

        public DbSet<About> Abouts { get; set; }

        public DbSet<CourseFeatures> CourseFeatures { get; set; }

        public DbSet<Notice> Notices { get; set; }


        public DbSet<Testimonial> Testimonials { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Subscripe> Subscripes { get; set; }

        public DbSet<Comment> Comments { get; set; }






















    }
}
