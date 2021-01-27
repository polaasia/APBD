using Lab_09.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private s19188Context db = new s19188Context();
        
        
        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(db.Students.ToList());
        }

        [HttpPost]
        public IActionResult AddStudent() 
        {
            /*
             var p = new Prescription()
            {
                Date = DateTime.Now,
                DueDate = DateTime.Now,
                IdPatient = 2
            };
            var set = new HashSet<Prescription> { p };

            var d = new Doctor
            {
                FirstName="AAA",
                LastName="BBB",
                Email="AAA@wp.pl",
                Prescription= set
            };
            db.Doctor.Add(d);

            db.SaveChanges(); //1 transakcja -> 2 INS
             */
            return Ok();

        }

        [HttpPut]
        public IActionResult ModifyStudent() 
        {
            return Ok();
            /*
             var d1 = new Doctor
            {
                IdDoctor=5,
                LastName="Kwiatkowski"
            };
            db.Attach(d1); // d1 znajduje sie pod system sledzenie zmian
                           // unchanged
                           //db.Add(d1);
            //db.Entry(d1).Property("LastName").IsModified = true;
            //db.Entry(d1).State = EntityState.Modified;

            db.SaveChanges();
             */

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(String id)
        {
            var st = new Student
            {
                IndexNumber = id
            };
            db.Attach(st);
            db.Remove(st);

            await db.SaveChangesAsync();

            return Ok("Student deleted");

        }
        /*
         * Endpoint which returns list of students.
         * Endpoint which allows to insert new student into database.
         * Endpoint which allows to modify student data.
         * Endpoint which allows to delete student
         */


    }
}
