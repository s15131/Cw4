using System.Collections.Generic;
using System.Data.SqlClient;
using Cw4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cw4.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
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
        }
    }
}
    