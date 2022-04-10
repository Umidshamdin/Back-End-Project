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
        public string Date { get; set; }
        public string Header { get; set; }
        public string Time { get; set; }
        public string Location { get; set; }
        public string DetailImage { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }

        [NotMapped]
        [Required]
        public IFormFile Photo { get; set; }

        [NotMapped]
        [Required]
        public IFormFile DPhoto { get; set; }
    }
}
