using Lab_09.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_09.Services
{
    public interface IStudentDbService
    {
        void EnrollStudent(EnrollmentRequest request);
        void PromoteStudents(int semester, string studies);
    }
}
