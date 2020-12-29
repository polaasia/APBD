using Lab_03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_03.DAL
{
    public interface IDbService
    {
        public IEnumerable<Student> GetStudents();
    }
}
