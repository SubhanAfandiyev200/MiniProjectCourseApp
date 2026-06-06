using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IStudentService
    {
        void Create(Student student);
        Student GetById(int id);
        void Delete(int id);
        IEnumerable<Student> GetStudentsByAge(int minAge,int maxAge);
        IEnumerable<Student> GetStudentsByGroupId(int id);
        IEnumerable<Student> GetStudentsByNameOrSurname(string text);
    }
}
