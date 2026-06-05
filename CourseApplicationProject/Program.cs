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
op: ConsoleColor.Cyan.WriteToConsole("Select operation:\n 1 - Create group\n 2 - Delete group\n 3 - Get all groups\n 4 - Get groups by id\n");
    ConsoleColor.DarkYellow.WriteToConsole(" 5 - Create student\n 6 - Delete student\n 7 - Get student by id");
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
        case (int)Operations.CreateStudent:
            studentController.Create();
            break;
        case (int)Operations.DeleteStudent:
            studentController.Delete();
            break;
        case (int)Operations.GetStudentById:
            studentController.GetById();
            break;
    }
}
