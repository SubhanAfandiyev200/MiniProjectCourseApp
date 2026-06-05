using Domain.Models;
using Repository.Data;
using Repository.Repositories.Interfaces;
using Service.Exceptions;
using Service.Helpers.Extensions;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Service.Services
{
    public class GroupService:IGroupService
    {
        private readonly IGroupRepository _groupRepo;
        public GroupService(IGroupRepository groupRepo)
        {
            _groupRepo = groupRepo;
        }
        //private readonly IStudentRepository _studentRepo;
        //public GroupService(IStudentRepository studentRepo)
        //{
        //    _studentRepo = studentRepo;
        //}
        public void Create(Group group)
        {
            _groupRepo.Create(group);
        }
        public List<Group> GetAllGroups()
        {
            return _groupRepo.GetAllGroups();
        }
        public Group GetById(int id)
        {
            return _groupRepo.GetById(id);
        }
        //public void Delete(int id)
        //{
        //    Group group = _groupRepo.GetById(id);
        //    if (group is null) throw new NotFoundException("Group not found!");
        //    _groupRepo.Delete(group);
        //    var result = AppDbContext<Student>.datas;
        //    Student student = _studentRepo.GetById
        //    foreach (var item in result)
        //    {
        //        if (item.Group.Id == id)
        //        {
        //            _studentRepo.Delete();
        //        }
        //    }
        //}
        public void Delete(int id)
        {
            var group = _groupRepo.GetById(id);
            if (group is null) throw new NotFoundException("Group not found");
            var students = _studentRepo.GetAll().Where(m => m.Group.Id == id);
            
        }
    }
}
