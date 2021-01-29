using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_09.DTOs
{
    public class PromoteRequest
    {
        [Required]
        public string Studies { get; set; }
        public int Semester { get; set; }
    }
}
