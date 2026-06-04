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
        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
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
            Student students = new()
            {
                Name = studentName,
                Surname = studentSurname,
                Age = studentAge,
                Email = studentEmail
            };
            _studentService.Create(students);
            ConsoleColor.Green.WriteToConsole("Student is successfully created!");
        }
    }

}
