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
        private readonly IStudentRepository _studentRepo;
        public GroupService(IGroupRepository groupRepo, IStudentRepository studentRepo)
        {
            _groupRepo = groupRepo;
            _studentRepo = studentRepo;
        }
        public void Create(Group group)
        {
            _groupRepo.Create(group);
        }
        public List<Group> GetAll()
        {
            return _groupRepo.GetAll();
        }
        public Group GetById(int id)
        {
            return _groupRepo.GetById(id);
        }
        public void Delete(int id)
        {
            var group = _groupRepo.GetById(id);
            if (group is null)
            {
                throw new NotFoundException("Group not found");
            }
            var students = _studentRepo.GetAll().Where(m => m.Group != null && m.Group.Id == id).ToList();
            foreach (var item in students)
            {
                _studentRepo.Delete(item);
            }
            _groupRepo.Delete(group);
        }
        public IEnumerable<Group> GetAllGroupsByTeacher(string fullName)
        {
            var result = _groupRepo.GetAllWithCondition(m => m.TeacherFullName.Contains(fullName));
            return result;
        }
        public IEnumerable<Group> GetAllGroupsbyRoom(string roomName)
        {
            var result = _groupRepo.GetAllWithCondition(m => m.RoomName.Contains(roomName));
            return result;
        }
        public IEnumerable<Group> GetAllGroupsByName(string name)
        {
            var result = _groupRepo.GetAllWithCondition(m => m.RoomName.Contains(name));
            return result;
        }
    }
}
