using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Models
{
    public class Event
    {

        public int Id { get; set; }

        public string Image { get; set; }

        public string Time { get; set; }

        public string Header { get; set; }

        public string DayTime { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        [Required, NotMapped]
        public IFormFile Photo { get; set; }
    }
}
