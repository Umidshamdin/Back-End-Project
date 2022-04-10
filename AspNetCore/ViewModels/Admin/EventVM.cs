using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.ViewModels
{
    public class EventVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        [NotMapped]
        [Required]
        public IFormFile Photo { get; set; }  
        public DateTime Time { get; set; }
        public string Header { get; set; } 
        public string DayTime { get; set; }
        public string Address { get; set; }
        public string DetailImage { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [NotMapped]
        [Required]
        public IFormFile DetailPhoto { get; set; }
    }
}
