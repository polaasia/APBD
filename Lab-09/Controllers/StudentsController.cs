using Lab_09.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_09.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly s19188Context _context;

        public StudentsController(s19188Context context)
        {
            _context = context;

        }


        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(_context.Students.ToList());
        }

        
        [HttpGet("{indexNumber}")]
        public IActionResult GetStudent(String indexNumber) 
        {
            var st = _context.Students.Where(s => s.IndexNumber.Equals(indexNumber)).FirstOrDefault();
            return Ok(st);
        }

        [HttpPost]
        public IActionResult AddStudent(Student student) 
        {
            var st = new Student
            {
                IndexNumber = student.IndexNumber,
                FirstName = student.FirstName,
                LastName = student.LastName,
                BirthDate = student.BirthDate,
                IdEnrollment = student.IdEnrollment
            };
            _context.Students.Add(st);
            _context.SaveChanges();
     
            return Ok();

        }

        [HttpPut("{indexNumber}")]
        public IActionResult ModifyStudent(String indexNumber) 
        {

            var st = new Student
            {
                IndexNumber = indexNumber,
                LastName = "Kwiatkowski"
            };

            _context.Attach(st);
            _context.Entry(st).Property("LastName").IsModified = true;
            _context.Entry(st).State = EntityState.Modified;

            _context.SaveChanges();
            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(String id)
        {
            var st = new Student
            {
                IndexNumber = id
            };
            _context.Attach(st);
            _context.Remove(st);

            await _context.SaveChangesAsync();

            return Ok("Student deleted");

        }
        
    }
}
