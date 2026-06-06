using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApplicationProject.Enums
{
    internal enum Operations
    {
        CreateGroup = 1,
        DeleteGroup,
        GetAllGroups,
        GetGroupsById,
        GetAllGroupsByTeacher,
        GetAllGroupsByRoom,
        GetAllGroupsByName,
        UpdateGroup,
        CreateStudent,
        DeleteStudent,
        GetStudentById,
        GetStudentsByAge,
        GetStudentsByGroupId,
        GetStudentsByNameOrSurname,
        UpdateStudent
    }
}
