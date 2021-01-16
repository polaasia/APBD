using Lab_03.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_03.DAL
{
    public class MockDbService : IDbService
    {
        private static IEnumerable<Student> _students;
        static MockDbService()
        {
            _students = new List<Student>
            {
                new Student{ FirstName="Jan", LastName="Kowalski"},
                new Student{ FirstName="Anna", LastName="Malewski"},
                new Student{ FirstName="Andrzej", LastName="Andrzejewicz"}
            };
        }

        public Student GetStudentByIndex(string index)
        {

            if (index == "s1234") 
            {
                return new Student { IndexNumber = "1", FirstName = "Andrzej", LastName = "Kowalski" };
            }

            return null;
        }

        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }

        public void SaveLogData(string[] textToLog)
        {
            string fileName = " requestsLog.txt";
            using (FileStream fs = new FileStream(fileName, FileMode.Append))
            {
                File.WriteAllLinesAsync(fileName, textToLog);

            }
        }
    }
}
