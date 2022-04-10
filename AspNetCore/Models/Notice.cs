using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Models
{
    public class Notice
    {
        public int Id { get; set; }

        public DateTime Time { get; set; }

        public string Description { get; set; }

    }
}
