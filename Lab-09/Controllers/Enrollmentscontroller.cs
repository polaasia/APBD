using Lab_09.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Lab_09.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {

        private readonly s19188Context _context;

        public EnrollmentsController(s19188Context context)
        {
            _context = context;

        }

        //enroll student
        [HttpPost("enroll")]
        public IActionResult EnrollStudent()
        {

            return Ok();
        }

        //promote student
        [HttpPut("promote")]
        public IActionResult PromoteStudents(int semester, string studies)
        {

            return Ok();


        }
       
    }
}
