using AspNetCore.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.ViewModels
{
    public class CourseVM
    {

        public List<Course> Courses { get; set; }
        public List<CourseFeatures> CourseFeatures { get; set; }





    }
}
