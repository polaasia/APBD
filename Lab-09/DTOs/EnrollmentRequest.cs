using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_09.DTOs
{
    public class EnrollmentRequest
    {
        [Required] //annotation
        public string IndexNumber { get; set; }

        [Required]
        [MaxLength(100)] //max length same as in database
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        public DateTime Birthdate { get; set; }

        [Required]
        public string Studies { get; set; }
    }
}
