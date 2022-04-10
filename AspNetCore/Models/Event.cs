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
        [Required]
        public string Image { get; set; }
        [NotMapped]
        [Required]
        public IFormFile Photo { get; set; }
        public DateTime Time { get; set; }
        public string Header { get; set; }
        public string DayTime { get; set; }
        public string Address { get; set; }

        public int EventDetailId { get; set; }

        public EventDetail EventDetail
        {
            get; set;

        }
    }
}
