using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        public string Image { get; set; }

        [NotMapped]
        [Required]
        public IFormFile Photo { get; set; }
        public string Name { get; set; }

        public string Posinition { get; set; }

        public string About { get; set; }

        public string Degree { get; set; }
        public string Experience { get; set; }
        public string Hobby { get; set; }
        public string Faculty { get; set; }

        public string Email { get; set; }

        public int PhoneNumber { get; set; }

        public string SkillName { get; set; }

       



    }
}
