using Domain.Models;
using Repository.Repositories.Interfaces;
using Service.Exceptions;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Service.Services
{
    public class StudentService :IStudentService
    {
        private readonly IStudentRepository _studentRepo;
        public StudentService(IStudentRepository studentRepo)
        {
            _studentRepo = studentRepo;
        }
        public void Create(Student student)
        {
            _studentRepo.Create(student);
        }
        public Student GetById(int id)
        {
            return _studentRepo.GetById(id);
        }
        public void Delete(int id)
        {
            var student = _studentRepo.GetById(id);
            if (student is null)
            {
                throw new NotFoundException("Group not found");
            }
            _studentRepo.Delete(student);
        }
        public IEnumerable<Student> GetStudentByAge(int minAge, int maxAge)
        {
            var result = _studentRepo.GetAllWithCondition(m => m.Age > minAge && m.Age < maxAge).ToList();
            return result;
        }
    }
}
