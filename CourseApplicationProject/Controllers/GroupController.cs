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
using System.Xml.Linq;
using Group = Domain.Models.Group;

namespace CourseApplicationProject.Controllers
{
    public class GroupController
    {
        private readonly IGroupService _groupService;
        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        public void Create()
        {
        groupName: ConsoleColor.Cyan.WriteToConsole("Enter group's name:");
            string groupName = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(groupName))
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.Empty);
                goto groupName;
            }
        fullName: ConsoleColor.Cyan.WriteToConsole("Enter teacher's full name:");
            string fullName = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(fullName))
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.Empty);
                goto fullName;
            }
            bool isCorrectFullNameFormat = Regex.IsMatch(fullName, @"^[\p{L}\s]+$");
            if (!isCorrectFullNameFormat)
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.WrongInput);
                goto fullName;
            }
        roomName: ConsoleColor.Cyan.WriteToConsole("Enter room's name:");
            string roomName = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(roomName))
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.Empty);
                goto roomName;
            }
            Group groups = new()
            {
                Name = groupName,
                TeacherFullName = fullName,
                RoomName = roomName
            };
            _groupService.Create(groups);
            ConsoleColor.Green.WriteToConsole("Group is successfully created!");
        }
        public void GetAll()
        {
            var result = _groupService.GetAll();
            if(result.Count == 0)
            {
                ConsoleColor.Red.WriteToConsole("No group exists.");
                return;
            }
            foreach(var item in result)
            {
                ConsoleColor.DarkYellow.WriteToConsole($"Id:{item.Id}, Name:{item.Name}, Teacher's full name:{item.TeacherFullName}, Room name:{item.RoomName}");
            }
            _groupService.GetAll();
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
            var result = _groupService.GetById(id);
            ConsoleColor.DarkYellow.WriteToConsole($"Id:{result.Id}, Name:{result.Name}, Teacher's full name:{result.TeacherFullName}, Room name:{result.RoomName}");
        }
        public void Delete()
        {
        id: ConsoleColor.Cyan.WriteToConsole("Enter the id which you want to delete:");
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
            _groupService.Delete(id);
            ConsoleColor.Green.WriteToConsole("Group and its students are successfully deleted!");
        }
        public void GetAllGroupsByTeacher()
        {
        nameSearch: ConsoleColor.Cyan.WriteToConsole("Enter teacher's full name to search group:");
            string fullName = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(fullName))
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.Empty);
                goto nameSearch;
            }
            var result = _groupService.GetAllGroupsByTeacher(fullName);
            foreach (var item in result)
            {
                ConsoleColor.DarkYellow.WriteToConsole($"Id:{item.Id}, Teacher's full name:{item.TeacherFullName},  Name:{item.Name}, Room name:{item.RoomName}");
            }
        }
        public void GetAllGroupsByRoom()
        {
        roomName: ConsoleColor.Cyan.WriteToConsole("Enter room's name to search group:");
            string roomName = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(roomName))
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.Empty);
                goto roomName;
            }
            var result = _groupService.GetAllGroupsByRoom(roomName);
            foreach (var item in result)
            {
                ConsoleColor.DarkYellow.WriteToConsole($"Id:{item.Id},Room name:{item.RoomName}, Teacher's full name:{item.TeacherFullName},  Name:{item.Name}");
            }
        }
        public void GetAllGroupsByName()
        {
        groupName: ConsoleColor.Cyan.WriteToConsole("Enter group's name to search:");
            string groupName = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(groupName))
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.Empty);
                goto groupName;
            }
            var result = _groupService.GetAllGroupsByName(groupName);
            foreach (var item in result)
            {
                ConsoleColor.DarkYellow.WriteToConsole($"Id:{item.Id}, Name:{item.Name}, Room name:{item.RoomName}, Teacher's full name:{item.TeacherFullName}");
            }
        }
    }
}
