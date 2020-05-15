using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Cw4.DTOs;
using Cw4.Models;
using Cw4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Cw4.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly Cw10DbContext _context;
        public StudentsController (Cw10DbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(_context.Student.ToList());
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteStudent(string id)
        {
            _context.Student.Remove(_context.Student.Where(x => x.IndexNumber == id).FirstOrDefault());
            _context.SaveChanges();
            return Ok("Usunięto studenta "+id);
        }
        [HttpPost("password/{id}")]
        public IActionResult ResetPassword(string id)
        {
           Random generuj=new Random();
           var student = _context.Student.Where(x => x.IndexNumber == id).FirstOrDefault();
           student.Password = generuj.Next(10123, 998999).ToString();
           _context.SaveChanges();
            return Ok("Wygenerowoano haslo dla " + id);
        }
        

            /* public IConfiguration Configuration { get; set; }
             public StudentsController(IConfiguration configuration)
             {
                 configuration = configuration;
             }
             private const string ConString = "Data Source=db-mssql;Initial Catalog=s15131;Integrated Security=True";
             [HttpGet]
             public IActionResult GetStudents()
             {
                 var student = new List<Student>();
                 using (SqlConnection con = new SqlConnection(ConString))
                 using (SqlCommand com = new SqlCommand())
                 {
                     com.Connection = con;
                     com.CommandText = "SELECT * FROM Student";
                     con.Open();

                     SqlDataReader dr = com.ExecuteReader();
                     while (dr.Read())
                     {
                         var st = new Student();
                         st.FirstName = dr["FirstName"].ToString();
                         st.LastName = dr["LastName"].ToString();
                         st.IndexNumber = dr["IndexNumber"].ToString();
                         st.BirthDate = dr["BirthDate"].ToString();
                         student.Add(st);
                     }

                 }
                 return Ok(student);
             }
             [HttpGet("{id}")]
             public IActionResult GetStudentEnrollments(string id)
             {
                 var enrollment = new List<Enrollment>();
                 using (SqlConnection con = new SqlConnection(ConString))
                 using (SqlCommand com = new SqlCommand())
                 {
                     com.Connection = con;
                     com.CommandText = "SELECT * FROM Enrollment e LEFT JOIN Student s ON e.IdEnrollment=s.IdEnrollment WHERE s.IndexNumber = @id";
                     com.Parameters.AddWithValue("id", id);
                     con.Open();
                     SqlDataReader dr = com.ExecuteReader();
                     while (dr.Read())
                     {
                         var enroll = new Enrollment();
                         enroll.IdEnrollment = dr["IdEnrollment"].ToString();
                         enroll.Semester = dr["Semester"].ToString();
                         enroll.IdStudy = dr["IdStudy"].ToString();
                         enroll.StartDate = dr["StartDate"].ToString();
                         enrollment.Add(enroll);
                     }
                 }
                 return Ok(enrollment);
             }*/



        }
    }
    