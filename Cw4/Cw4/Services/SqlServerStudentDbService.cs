using Cw4.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cw4.Services
{

    public class SqlServerStudentDbService : IStudentDbService

    {
        public string GetName(string index)
        {
            using (var ConString = new SqlConnection("Data Source=db-mssql;Initial Catalog=s15131;Integrated Security=True"))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = ConString;
                com.CommandText = "SELECT * FROM Student WHERE IndexNumber=@index";
                com.Parameters.AddWithValue("index", index);
                ConString.Open();
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    return dr["FirstName"].ToString()+ dr["LastName"].ToString();


                }
            }



            return null;
        }

        public string GetStudent(string index)
        {
            using (var ConString = new SqlConnection("Data Source=db-mssql;Initial Catalog=s15131;Integrated Security=True"))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = ConString;
                com.CommandText = "SELECT * FROM Student WHERE IndexNumber=@index";
                com.Parameters.AddWithValue("index", index);
                ConString.Open();
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    return dr["IndexNumber"].ToString();


                }
            }



            return null;
        }

        public bool Logowanie(string index, string haslo)
        {
            using (var ConString = new SqlConnection("Data Source=db-mssql;Initial Catalog=s15131;Integrated Security=True"))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = ConString;
                com.CommandText = "SELECT * FROM Student WHERE IndexNumber=@index";
                com.Parameters.AddWithValue("index", index);
                SqlDataReader dr = com.ExecuteReader();
                string index1;
                string haslo1=null;
                if (dr.Read())
                {
                    index1 = dr["IndexNumber"].ToString();
                    haslo1 = dr["Password"].ToString();
                }
                bool zgodne = haslo == haslo1;
                return zgodne;
            }
        }
    }
    }
