using Lab_03.DAL;
using Lab_03.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_03.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private string ConnString = "Data Source=db-mssql;Initial Catalog=s19188;Integrated Security=True";

        private readonly IDbService _dbService;
        public StudentsController(IDbService dbService) 
        {
            _dbService = dbService;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            var result = new List<Student>();
           

            using (SqlConnection con = new SqlConnection(ConnString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from student";

                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var st = new Student();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                    result.Add(st);
                }

                return Ok(result);
            }
        }

        [HttpGet("{IndexNumber}")]
        public IActionResult GetStudent(string IndexNumber)
        {
            using (SqlConnection con = new SqlConnection(ConnString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from student where IndexNumber=@index";

                /*
                 * SqlParameter par1 = new SqlParameter();
                 * par1.ParameterName = "index";
                 * par1.Value = IndexNumber;
                 * com.Parameters.Add(par1);
                 */

                com.Parameters.AddWithValue("index", IndexNumber);

                con.Open();

                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var st = new Student();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                    return Ok(st);
                }

                return Ok();
            }
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }

        [HttpPut("{id}")]
        public IActionResult PutStudent(int id)
        {
            if (0 < id && id < 20) 
            {
                return Ok("Update complete");
            }
            return NotFound("Student not found");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id) 
        {
            if (0 < id && id < 20)
            {
                return Ok("Update complete");
            }
            return NotFound("Student not found");
        }
    }
}
