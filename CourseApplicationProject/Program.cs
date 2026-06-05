using CourseApplicationProject.Controllers;
using Repository.Repositories;
using Service.Helpers.Constants;
using Service.Helpers.Extensions;
using Service.Services;
GroupRepository groupRepository = new();
GroupService groupService = new(groupRepository);
GroupController groupController = new(groupService);
StudentRepository studentRepository = new();
StudentService studentService = new(studentRepository);
StudentController studentController = new(studentService);
while (true)
{
op: ConsoleColor.Cyan.WriteToConsole("1- create grup,2-create student");
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
        case 1:
            groupController.Create();
            break;
    }
}
