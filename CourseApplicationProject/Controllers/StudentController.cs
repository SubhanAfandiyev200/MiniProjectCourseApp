using Domain.Models;
using Service.Exceptions;
using Service.Helpers.Constants;
using Service.Helpers.Extensions;
using Service.Services;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Group = Domain.Models.Group;

namespace CourseApplicationProject.Controllers
{
    public class StudentController
    {
        private readonly IStudentService _studentService;
        private readonly IGroupService _groupService;

        public StudentController(IStudentService studentService, IGroupService groupService)
        {
            _studentService = studentService;
            _groupService = groupService;
        }
        public void Create()
        {
        studentName: ConsoleColor.Cyan.WriteToConsole("Enter student's name:");
            string studentName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(studentName))
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.Empty);
                goto studentName;
            }
            bool isCorrectNameFormat = Regex.IsMatch(studentName, @"^[\p{L}\s]+$");
            if (!isCorrectNameFormat)
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.WrongInput);
                goto studentName;
            }
        studentSurname: ConsoleColor.Cyan.WriteToConsole("Enter student's surname:");
            string studentSurname = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(studentSurname))
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.Empty);
                goto studentSurname;
            }
            bool isCorrectSurnameFormat = Regex.IsMatch(studentSurname, @"^[\p{L}\s]+$");
            if (!isCorrectSurnameFormat)
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.WrongInput);
                goto studentSurname;
            }
        studentAge: ConsoleColor.Cyan.WriteToConsole("Enter student's age:");
            string studentAgeStr = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(studentAgeStr))
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.Empty);
                goto studentAge;
            }
            bool studentAgeIsNumber = int.TryParse(studentAgeStr, out int studentAge);
            if (!studentAgeIsNumber)
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.WrongInput);
                goto studentAge;
            }
            if (!(studentAge >= 15 && studentAge <= 60))
            {
                ConsoleColor.Red.WriteToConsole("Student age must be between 15 and 60");
                goto studentAge;
            }
        studentEmail: ConsoleColor.Cyan.WriteToConsole("Enter student's email:");
            string studentEmail = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(studentEmail))
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.Empty);
                goto studentEmail;
            }
            bool isValidEmail = Regex.IsMatch(studentEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!isValidEmail)
            {
                ConsoleColor.Red.WriteToConsole("Email format is wrong(For example: you@gmail.com).Please try again:");
                goto studentEmail;
            }
            var stuEmail = _groupService.GetAll().FirstOrDefault(m => m.Name.Equals(studentEmail.Trim(), StringComparison.OrdinalIgnoreCase));
            if (stuEmail != null)
            {
                ConsoleColor.Red.WriteToConsole("Email already exists!");
                return;
            }
        group: ConsoleColor.Cyan.WriteToConsole("Enter group's Id:");
            string groupId = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(groupId))
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.Empty);
                goto group;
            }
            bool isCorrectFormatGroup = int.TryParse(groupId, out int id);
            if (!isCorrectFormatGroup)
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.WrongInput);
                goto group;
            }
            try
            {
                var groupResult = _groupService.GetById(id);
                Student students = new()
                {
                    Name = studentName,
                    Surname = studentSurname,
                    Age = studentAge,
                    Email = studentEmail,
                    Group = groupResult
                };
                _studentService.Create(students);
                ConsoleColor.Green.WriteToConsole("Student is successfully created!");
            }
            catch (NotFoundException ex)
            {
                ConsoleColor.Red.WriteToConsole(ex.Message);
                return;
            }
        }
        public void GetById()
        {
        id: ConsoleColor.Cyan.WriteToConsole("Enter the id which you want to get:");
            string idStr = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(idStr))
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.Empty);
                goto id;
            }
            bool idIsCorrectFormat = int.TryParse(idStr, out int id);
            if (!idIsCorrectFormat)
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.WrongInput);
                goto id;
            }
            try
            {
                var result = _studentService.GetById(id);
                ConsoleColor.DarkYellow.WriteToConsole($"Id:{result.Id}, Name:{result.Name}, Surname:{result.Surname}, Age:{result.Age}, Email:{result.Email}, Group:{result.Group.Name}");
            }
            catch (NotFoundException ex)
            {
                ConsoleColor.Red.WriteToConsole(ex.Message);
                return;
            }
        }
        public void Delete()
        {
        idDelete: ConsoleColor.Cyan.WriteToConsole("Enter the id which you want to delete:");
            string idStr = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(idStr))
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.Empty);
                goto idDelete;
            }
            bool idDeleteIsCorrectFormat = int.TryParse(idStr, out int idDeleter);
            if (!idDeleteIsCorrectFormat)
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.WrongInput);
                goto idDelete;
            }
            try
            {
                _studentService.Delete(idDeleter);
            }
            catch (NotFoundException ex)
            {
                ConsoleColor.Red.WriteToConsole(ex.Message);
                return;
            }

            ConsoleColor.Green.WriteToConsole("Id is successfully deleted!");
        }
        public void GetStudentsByAge()
        {
        age: ConsoleColor.Cyan.WriteToConsole("Add min age:");
            string minAge = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(minAge))
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.Empty);
                goto age;
            }
            bool minAgeisCorrectFormat = int.TryParse(minAge, out int min);
            if (!minAgeisCorrectFormat)
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.WrongInput);
                goto age;
            }
            ConsoleColor.Cyan.WriteToConsole("Enter max age:");
            string maxAge = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(maxAge))
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.Empty);
                goto age;
            }
            bool maxAgeIsCorrectFormat = int.TryParse(maxAge, out int max);
            if (!maxAgeIsCorrectFormat)
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.WrongInput);
                goto age;
            }
            if (min > max)
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.WrongInput);
                goto age;
            }
            try
            {
                var result = _studentService.GetStudentsByAge(min, max);
                foreach (var item in result)
                {
                    ConsoleColor.DarkYellow.WriteToConsole($"Id:{item.Id}, Name:{item.Name}, Surname:{item.Surname}, Age:{item.Age}, Email:{item.Email}, Group:{item.Group.Name}");
                }
            }
            catch (NotFoundException ex)
            {
                ConsoleColor.Red.WriteToConsole(ex.Message);
                return;
            }
        }
        public void GetStudentsByGroupId()
        {
        id: ConsoleColor.Cyan.WriteToConsole("Enter the group's id which you want to get:");
            string idStr = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(idStr))
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.Empty);
                goto id;
            }
            bool idIsCorrectFormat = int.TryParse(idStr, out int id);
            if (!idIsCorrectFormat)
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.WrongInput);
                goto id;
            }
            try
            {
                var result = _studentService.GetStudentsByGroupId(id);
                foreach (var item in result)
                {
                    ConsoleColor.DarkYellow.WriteToConsole($"Id:{item.Id}, Name:{item.Name}, Surname:{item.Surname}, Age:{item.Age}, Email:{item.Email}, Group:{item.Group.Name}");
                }
            }
            catch (NotFoundException ex)
            {
                ConsoleColor.Red.WriteToConsole(ex.Message);
                return;
            }
        }
        public void GetStudentsByNameOrSurname()
        {
            ConsoleColor.Cyan.WriteToConsole("Enter student's name or surname to search:");
            string text = Console.ReadLine();
            try
            {
                var result = _studentService.GetStudentsByNameOrSurname(text);
                foreach (var item in result)
                {
                    ConsoleColor.DarkYellow.WriteToConsole($"Id:{item.Id}, Name:{item.Name}, Surname:{item.Surname}, Age:{item.Age}, Email:{item.Email}, Group:{item.Group.Name}");
                }
            }
            catch (NotFoundException ex)
            {
                ConsoleColor.Red.WriteToConsole(ex.Message);
                return;
            }
        }
        public void Update()
        {
        idInput: ConsoleColor.Cyan.WriteToConsole("Enter student's id to update: ");
            string idStr = Console.ReadLine();
            if (!int.TryParse(idStr, out int id))
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.WrongInput);
                goto idInput;
            }
        name: ConsoleColor.Cyan.WriteToConsole("Enter student's name:");
            string name = Console.ReadLine();
            bool nameIsCorrectFormat = int.TryParse(name, out int nameStr);
            if(nameIsCorrectFormat)
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.WrongInput);
                goto name;
            }
        surname: ConsoleColor.Cyan.WriteToConsole("Enter student's surname:");
            string surname = Console.ReadLine();
            bool surnameIsCorrectFormat = int.TryParse(surname, out int surnameStr);
            if(surnameIsCorrectFormat)
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.WrongInput);
                goto surname;
            }
            email: ConsoleColor.Cyan.WriteToConsole("Enter student's email:");
            string email = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(email))
            {
                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!Regex.IsMatch(email, pattern))
                {
                    ConsoleColor.Red.WriteToConsole(ValidationMessages.WrongInput);
                    goto email;
                }
            }
            var stuEmail = _groupService.GetAll().FirstOrDefault(m => m.Name.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase));
            if (stuEmail != null)
            {
                ConsoleColor.Red.WriteToConsole("Email already exists!");
                return;
            }
        age: ConsoleColor.Cyan.WriteToConsole("Enter student's age:");
            string ageStr = Console.ReadLine();
            int age = -1;
            if (!string.IsNullOrWhiteSpace(ageStr))
            {
                if (!int.TryParse(ageStr, out age))
                {
                    ConsoleColor.Red.WriteToConsole(ValidationMessages.WrongInput);
                    goto age;
                }
                if (!(age >= 15 && age <= 60))
                {
                    ConsoleColor.Red.WriteToConsole("Student's age must be between 15 and 60");
                    goto age;
                }
            }
        group: ConsoleColor.Cyan.WriteToConsole("Enter group's id which you want to move student:");
            string groupIdStr = Console.ReadLine();
            Group group = null;
            if (!string.IsNullOrWhiteSpace(groupIdStr))
            {
                bool isCorrectIdFormat = int.TryParse(groupIdStr, out int groupId);

                if (!isCorrectIdFormat)
                {
                    ConsoleColor.Red.WriteToConsole(ValidationMessages.WrongInput);
                    goto group;
                }
                try
                {
                    group = _groupService.GetById(groupId);
                    if (group == null)
                    {
                        throw new NotFoundException("Group with this id was not found");
                    }
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteToConsole(ex.Message);
                    return;
                }
            }

            Student student = new()
            {
                Name = name,
                Surname = surname,
                Email = email,
                Age = age,
                Group = group
            };
            try
            {
                _studentService.Update(id, student);
                ConsoleColor.Green.WriteToConsole("Student updated successfully!");
            }
            catch (NotFoundException ex)
            {
                ConsoleColor.Red.WriteToConsole(ex.Message);
            }
        }
    }
}
