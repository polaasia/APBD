using Lab_03.DAL;
using Lab_03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Lab_03.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private string ConnString = "Data Source=db-mssql;Initial Catalog=s19188;Integrated Security=True";

        private readonly IDbService _dbService;

  
        public IConfiguration _Configuration { get; set; }

        public StudentsController(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

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

        //logging endpoint
        [HttpPost]
        public IActionResult Login(LoginRequestDto request)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, "jan123"),
                new Claim(ClaimTypes.Role, "admin"),
                new Claim(ClaimTypes.Role, "student")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: "Gakko",
                audience: "Students",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = Guid.NewGuid()
            });
        }
    }
}
