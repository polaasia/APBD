using Lab_09.DTOs;
using Lab_09.Models;
using Lab_09.Services;
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
        private readonly IStudentDbService _service;
        public EnrollmentsController(IStudentDbService service)
        {
            _service = service;
        }

        //enroll student
        [HttpPost]
        public IActionResult EnrollStudent(EnrollmentRequest request)
        {
            _service.EnrollStudent(request);

            var response = new EnrollmentResponse();
            return Ok(response);
        }

        //promote student
        [HttpPost("promotions")]
        public IActionResult PromoteStudents(int semester, string studies)
        {
            _service.PromoteStudents(semester, studies);

            var response = new PromoteResponse();
            return Ok(response);
        }
    }
}
