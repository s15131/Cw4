using Cw4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw4.Services
{
    public interface IStudentDbService
    {
        

        public string GetStudent(string index);
        public string GetName(string index);
        public bool Logowanie(string index, string haslo);


    }
}
