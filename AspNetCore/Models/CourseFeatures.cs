using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Models
{
    public class CourseFeatures
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public int Duration { get; set; }
        public int ClassDuration { get; set; }
        public string Level { get; set; }
        public string Language { get; set; }
        public int StudentCount { get; set; }
        public string Assesments { get; set; }
        public string Prise { get; set; }

    }
}
