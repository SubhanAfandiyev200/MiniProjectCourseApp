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
            var result = _studentRepo.GetById(id);
            if(result is null)
            {
                throw new NotFoundException("Student not found");
            }
            return result;
        }
        public void Delete(int id)
        {
            var student = _studentRepo.GetById(id);
            if (student is null)
            {
                throw new NotFoundException("No student found with typed id!");
            }
            _studentRepo.Delete(student);
        }
        public IEnumerable<Student> GetStudentsByAge(int minAge, int maxAge)
        {

            var result = _studentRepo.GetAllWithCondition(m => m.Age > minAge && m.Age < maxAge).ToList();
            if(result.Count == 0)
            {
                throw new NotFoundException("No students found in this age range!");
            }
            return result;
        }
        public IEnumerable<Student> GetStudentsByGroupId(int id)
        {
            var result = _studentRepo.GetAllWithCondition(m => m.Group.Id == id).ToList();
            if(result.Count == 0)
            {
                throw new NotFoundException("No groups found with typed id!");
            }
            return result;
        }
        public IEnumerable<Student> GetStudentsByNameOrSurname(string text)
        {
            var result = _studentRepo.GetAllWithCondition(m => m.Name == text || m.Surname == text).ToList();
            if(result.Count == 0)
            {
                throw new NotFoundException("There aren't any students with this name or surname!");
            }
            return result;
        }
    }
}
