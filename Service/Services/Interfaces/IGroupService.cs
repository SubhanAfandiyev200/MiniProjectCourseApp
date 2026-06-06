using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Service.Services.Interfaces
{
    public interface IGroupService
    {
        void Create(Group group);
        List<Group> GetAll();
        Group GetById(int id);
        void Delete(int id);
        IEnumerable<Group> GetAllGroupsByTeacher(string fullName);
        IEnumerable<Group> GetAllGroupsByRoom(string roomName);
        IEnumerable<Group> GetAllGroupsByName(string name);
        
    }
}
