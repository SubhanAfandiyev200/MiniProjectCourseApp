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
        List<Group> GetAllGroups();
        Group GetById(int id);
        //Delete qalib yaz
    }
}
