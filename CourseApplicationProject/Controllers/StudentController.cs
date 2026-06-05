using Domain.Models;
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

namespace CourseApplicationProject.Controllers
{
    public class StudentController
    {
        private readonly IStudentService _studentService;
        private readonly IGroupService _groupService;
        public StudentController(IStudentService studentService,IGroupService groupService)
        {
            _studentService = studentService;
            _groupService = groupService;
        }
        public void Create()
        {
        studentName: ConsoleColor.Cyan.WriteToConsole("Enter student's name:");
            string studentName = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(studentName))
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
            if(string.IsNullOrWhiteSpace(studentAgeStr))
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.Empty);
                goto studentAge;
            }
            bool studentAgeIsNumber = int.TryParse(studentAgeStr, out int studentAge);
            if(!studentAgeIsNumber)
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.WrongInput);
                goto studentAge;
            }
        studentEmail: ConsoleColor.Cyan.WriteToConsole("Enter student's email:");
            string studentEmail = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(studentEmail))
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.Empty);
                goto studentEmail;
            }
            bool isValidEmail =Regex.IsMatch(studentEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!isValidEmail)
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.WrongInput);
                goto studentEmail;
            }
        group: ConsoleColor.Cyan.WriteToConsole("Enter group's Id:");
            string groupId = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(groupId))
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.Empty);
                goto group;
            }
            bool isCorrectFormatGroup = int.TryParse(groupId, out int id);
            if(!isCorrectFormatGroup)
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.WrongInput);
                goto group;
            }
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
        public void GetById()
        {
        id: ConsoleColor.Cyan.WriteToConsole("Enter the id which you want to get:");
            string idStr = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(idStr))
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.Empty);
                goto id;
            }
            bool idIsCorrectFormat = int.TryParse(idStr, out int id);
            if(!idIsCorrectFormat)
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.WrongInput);
                goto id;
            }
            var result = _studentService.GetById(id);
            ConsoleColor.DarkYellow.WriteToConsole($"Id:{result.Id}, Name:{result.Name}, Surname:{result.Surname}, Age:{result.Age}, Email:{result.Email}, Group:{result.Group}");
        }
    }

}
