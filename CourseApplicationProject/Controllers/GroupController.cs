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

namespace CourseApplicationProject.Controllers
{
    public class GroupController
    {
        private readonly IGroupService _groupService;
        public GroupController(GroupService groupService)
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
            if (!Regex.IsMatch(fullName, @"^[\p{L}\s]+$"))
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.WrongInput);
                goto fullName;
            }
        roomName: ConsoleColor.Cyan.WriteToConsole("Enter room's name:");
            string roomName = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(roomName))
            {
                ConsoleColor.Red.WriteToConsole(ValidationMessages.Empty);
            }
        }
    }
}
