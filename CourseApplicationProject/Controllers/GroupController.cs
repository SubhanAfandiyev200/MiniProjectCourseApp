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
            bool isValidGroupName = Regex.IsMatch(groupName, @"^[\p{L}0-9\s]{1,20}$");
            if (!isValidGroupName)
            {
                ConsoleColor.Red.WriteToConsole("Groups name can be max 20 symbol or format is wrong!");
                goto groupName;
            }
            var group = _groupService.GetAll().FirstOrDefault(m => m.Name.Equals(groupName.Trim(), StringComparison.OrdinalIgnoreCase));
            if (group != null)
            {
                ConsoleColor.Red.WriteToConsole("Group already exists!");
                return;
            }
        fullName: ConsoleColor.Cyan.WriteToConsole("Enter teacher's full name:");
            string fullName = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(fullName))
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.Empty);
                goto fullName;
            }
            bool isValidName = Regex.IsMatch(fullName, @"^[\p{L}\s]+$");
            if (!isValidName)
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
            bool isValidRoomName = Regex.IsMatch(roomName, @"^[\p{L}0-9\s]{1,20}$");
            if (!isValidGroupName)
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.WrongInput);
                goto roomName;
            }
            Group groups = new()
            {
                Name = groupName,
                TeacherFullName = fullName,
                RoomName = roomName
            };
            _groupService.Create(groups);
            ConsoleColor.Green.WriteToConsole("Room is created successfully!");
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
            try
            {
                var result = _groupService.GetById(id);
                ConsoleColor.DarkYellow.WriteToConsole($"Id:{result.Id}, Name:{result.Name}, Teacher's full name:{result.TeacherFullName}, Room name:{result.RoomName}");
            }
            catch (NotFoundException ex)
            {
                ConsoleColor.Red.WriteToConsole("Group with this id doesn't exist!");
                return; 
            }
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
            try
            {
               _groupService.Delete(id);
                
            }
           catch (NotFoundException ex)
            {
                ConsoleColor.Red.WriteToConsole(ex.Message);
                return;
            }
            ConsoleColor.Green.WriteToConsole("Group and its students are successfully deleted!");
        }
        public void GetAllGroupsByTeacher()
        {
            ConsoleColor.Cyan.WriteToConsole("Enter teacher's full name to search group:");
            string fullName = Console.ReadLine();
            try
            {
                var result = _groupService.GetAllGroupsByTeacher(fullName);
                foreach (var item in result)
                {
                    ConsoleColor.DarkYellow.WriteToConsole($"Id:{item.Id}, Teacher's full name:{item.TeacherFullName},  Name:{item.Name}, Room name:{item.RoomName}");
                }
            }
            catch (NotFoundException ex)
            {
                ConsoleColor.Red.WriteToConsole(ex.Message);
                return;
            }
        }
        public void GetAllGroupsByRoom()
        {
            ConsoleColor.Cyan.WriteToConsole("Enter room's name to search group:");
            string roomName = Console.ReadLine();
            try
            {
                var result = _groupService.GetAllGroupsByRoom(roomName);
                foreach (var item in result)
                {
                    ConsoleColor.DarkYellow.WriteToConsole($"Id:{item.Id},Room name:{item.RoomName}, Teacher's full name:{item.TeacherFullName},  Name:{item.Name}");
                }
            }
            catch (NotFoundException ex)
            {
                ConsoleColor.Red.WriteToConsole(ex.Message);
                return;
            }
        }
        public void GetAllGroupsByName()
        {
            ConsoleColor.Cyan.WriteToConsole("Enter group's name to search:");
            string groupName = Console.ReadLine();
            try
            {
                var result = _groupService.GetAllGroupsByName(groupName);
                foreach (var item in result)
                {
                    ConsoleColor.DarkYellow.WriteToConsole($"Id:{item.Id}, Name:{item.Name}, Room name:{item.RoomName}, Teacher's full name:{item.TeacherFullName}");
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
            try
            {
            id: ConsoleColor.Cyan.WriteToConsole("Enter the group's id to update: ");
                string idStr = Console.ReadLine();
                bool idIsCorrectFormat = int.TryParse(idStr, out int id);
                if (!idIsCorrectFormat)
                {
                    ConsoleColor.Red.WriteToConsole(ValidationMessages.WrongInput);
                    goto id;
                }
                ConsoleColor.Cyan.WriteToConsole("Enter group's name:");
                string name = Console.ReadLine();
                var group = _groupService.GetAll().FirstOrDefault(m => m.Name.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase));
                if (group != null)
                {
                    ConsoleColor.Red.WriteToConsole("Group already exists!");
                    return;
                }
                ConsoleColor.Cyan.WriteToConsole("Enter teacher's full name:");
            fullName: string fullName = Console.ReadLine();
                bool fullNameIsCorrectFormat = int.TryParse(fullName, out int fullNameStr);
                if(fullNameIsCorrectFormat)
                {
                    ConsoleColor.Red.WriteToConsole(ValidationMessages.WrongInput);
                    goto fullName;
                }
                ConsoleColor.Cyan.WriteToConsole("Enter room's name:");
                string roomName = Console.ReadLine();
                Group newGroup = new()
                {
                    Name = name,
                    TeacherFullName = fullName,
                    RoomName = roomName
                };
                _groupService.Update(id, newGroup);

                ConsoleColor.Green.WriteToConsole("Group successfully updated!");
            }
            catch (NotFoundException ex)
            {
                ConsoleColor.Red.WriteToConsole(ex.Message);
            }
        }
    }
}
