using Cw4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw4.Services
{
    public interface IStudentDbService
    {
        IEnumerable<Student> GetStudents();

        public string GetStudent(string index);



    }
}
