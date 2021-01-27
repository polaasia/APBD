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

        private s19188Context db = new s19188Context();

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
