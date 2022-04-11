using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Models
{
    public class Blog
    {    
        public int Id { get; set; }
        public string Image { get; set; }
        public string Author { get; set; }
        public DateTime Time { get; set; }
        public string BlogName { get; set; }

        [MinLength(13, ErrorMessage = "13-den asagi ola bilmez")]
        public string Description { get; set; }

        [Required, NotMapped]
        public IFormFile Photo { get; set; }
    }
}

