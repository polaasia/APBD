using Lab_09.DTOs;
using Lab_09.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Lab_09.Services
{
    public class SqlStudentService : IStudentDbService
    {

        private readonly s19188Context _context;
        public SqlStudentService(s19188Context context)
        {
            _context = context;
        }

        public void EnrollStudent(EnrollmentRequest request)
        {
            //chceck if there are studies like that
            var studies = _context.Studies.Where(s => s.Name.Equals(request.Studies)).FirstOrDefault();

            if (studies == null)
            {
                Console.WriteLine("studies do not exist");
            }

            //if there are chcheck if there is enrrolment for 1st semester
            var idStudies = studies.IdStudy;
            var enrollment = _context.Enrollments.Where(s => s.IdStudy == idStudies && s.Semester == 1).FirstOrDefault();

            //if not - create new enrollment 
            if (enrollment == null)
            {
                var en = new Enrollment
                {
                    Semester = 1,
                    IdStudy = idStudies,
                    StartDate = DateTime.Now
                };

                _context.Enrollments.Add(en);

            }

            //chceck if student with this id exists
            var student = _context.Students.Where(s => s.IndexNumber == request.IndexNumber).FirstOrDefault();

            //if not create new student and enroll them 
            if (student == null)
            {
                var st = new Student
                {
                    IndexNumber = request.IndexNumber,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    BirthDate = request.Birthdate,
                    IdEnrollment = enrollment.IdEnrollment
                };

                _context.Add(st);
            }

            _context.SaveChanges();

        }

        public void PromoteStudents(int semester, string studies)
        {
            int newSemester = semester + 1;
            var studID = _context.Studies.Where(s => s.Name.Equals(studies)).FirstOrDefault().IdStudy;
            
            var en = _context.Enrollments.Where(e => e.IdStudy == studID && e.Semester == semester).FirstOrDefault().IdEnrollment;

            var enUp = new Enrollment
            {
                IdEnrollment = en,
                Semester = newSemester
            };

            _context.Attach(enUp);
            _context.Entry(enUp).Property("Semester").IsModified = true;
            _context.Entry(enUp).State = EntityState.Modified;

            _context.SaveChanges();
        }
    }
}
