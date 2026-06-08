using Domain.Models;
using Repository.Data;
using Repository.Repositories.Interfaces;
using Service.Exceptions;
using Service.Helpers.Extensions;
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
                throw new NotFoundException("No students found with typed group id!");
            }
            return result;
        }
        public IEnumerable<Student> GetStudentsByNameOrSurname(string text)
        {
            var result = _studentRepo.GetAllWithCondition(m => string.IsNullOrWhiteSpace(text) || m.Name.Equals(text.Trim(), StringComparison.OrdinalIgnoreCase) || m.Surname.Equals(text.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
            if(result.Count == 0)
            {
                throw new NotFoundException("There aren't any students with this name or surname!");
            }
            return result;
        }
        public void Update(int id, Student student)
        {
            var result = _studentRepo.GetById(id);
            if (result == null)
            {
                throw new NotFoundException("Group with this id not found!");
            }
            if (!string.IsNullOrWhiteSpace(student.Name))
            {
                result.Name = student.Name;
            }
            if (!string.IsNullOrWhiteSpace(student.Surname))
            {
                result.Surname = student.Surname;
            }
            if (!string.IsNullOrWhiteSpace(student.Email))
            {
                result.Email = student.Email;
            }
            if(!(student.Age == -1))
            {
                result.Age = student.Age;
            }
            if(student.Group != null)
            {
                result.Group = student.Group;
            }
        }
        public List<Student> GetAll()
        {
            return _studentRepo.GetAll();
        }
    }
}
