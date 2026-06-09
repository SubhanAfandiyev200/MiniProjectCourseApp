using CourseApplicationProject.Controllers;
using CourseApplicationProject.Enums;
using Repository.Repositories;
using Service.Helpers.Constants;
using Service.Helpers.Extensions;
using Service.Services;
GroupRepository groupRepository = new();
StudentRepository studentRepository = new();
GroupService groupService = new(groupRepository, studentRepository);
GroupController groupController =new(groupService);
StudentService studentService =new(studentRepository);
StudentController studentController = new(studentService, groupService); 
while (true)
{
op: ConsoleColor.Cyan.WriteToConsole("Select group operation:\n 1 - Create group\n 2 - Delete group\n 3 - Get all groups\n 4 - Get groups by id\n 5 - Get groups by teacher name\n 6 - Get groups by room name\n 7 - Search groups by name\n 8 - Update group");
    ConsoleColor.Gray.WriteToConsole("--------------------------------------------------------------------------------------");
    ConsoleColor.Yellow.WriteToConsole("Select student operation:\n 9 - Create student\n 10 - Delete student\n 11 - Get student by id\n 12 - Get student by age\n 13 - Get student by group id\n 14 - Search student by name or surname\n 15 - Update student");
    string opStr = Console.ReadLine();
    if(string.IsNullOrWhiteSpace(opStr))
    {
        ConsoleColor.Red.WriteToConsole(ValidationMessages.Empty);
        goto op;
    }
    bool opIsCorrectFormat = int.TryParse(opStr, out int op);
    if(!opIsCorrectFormat)
    {
        ConsoleColor.Red.WriteToConsole(ValidationMessages.WrongInput);
        goto op;
    }
    switch (op)
    {
        case (int)Operations.CreateGroup:
            groupController.Create();
            break;
        case (int)Operations.DeleteGroup:
            groupController.Delete();
            break;
        case (int)Operations.GetAllGroups:
            groupController.GetAll();
            break;
        case (int)Operations.GetGroupsById:
            groupController.GetById();
            break;
        case (int)Operations.GetAllGroupsByTeacher:
            groupController.GetAllGroupsByTeacher();
            break;
        case (int)Operations.GetAllGroupsByRoom:
            groupController.GetAllGroupsByRoom();
            break;
        case (int)Operations.GetAllGroupsByName:
            groupController.GetAllGroupsByName();
            break;
        case (int)Operations.UpdateGroup:
            groupController.Update();
            break;
        case (int)Operations.CreateStudent:
            studentController.Create();
            break;
        case (int)Operations.DeleteStudent:
            studentController.Delete();
            break;
        case (int)Operations.GetStudentById:
            studentController.GetById();
            break;
        case (int)Operations.GetStudentsByAge:
            studentController.GetStudentsByAge();
            break;
        case (int)Operations.GetStudentsByGroupId:
            studentController.GetStudentsByGroupId();
            break;
        case (int)Operations.GetStudentsByNameOrSurname:
            studentController.GetStudentsByNameOrSurname();
            break;
        case (int)Operations.UpdateStudent:
            studentController.Update();
            break;
        default:
            ConsoleColor.Red.WriteToConsole("Operation not found!");
            break;
    }
}
