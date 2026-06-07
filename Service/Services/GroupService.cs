using Domain.Models;
using Repository.Data;
using Repository.Repositories;
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
            var result = _groupRepo.GetById(id);
            if (result is null)
            {
                throw new NotFoundException("Group with this id doesn't exist!");
            }
            return result;
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
            var result = _groupRepo.GetAllWithCondition(m => string.IsNullOrWhiteSpace(fullName) || m.TeacherFullName.Contains(fullName.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
            if(result.Count == 0)
            {
                throw new NotFoundException("Group with this teacher fullname not found!");
            }
            return result;
        }
        public IEnumerable<Group> GetAllGroupsByRoom(string roomName)
        {
            var result = _groupRepo.GetAllWithCondition(m => string.IsNullOrWhiteSpace(roomName) || m.RoomName.Contains(roomName.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
            if(result.Count == 0)
            {
                throw new NotFoundException("Group with this room name not found!");
            }
            return result;
        }
        public IEnumerable<Group> GetAllGroupsByName(string name)
        {
            var result = _groupRepo.GetAllWithCondition(m => string.IsNullOrWhiteSpace(name) || m.Name.Contains(name.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
            if(result.Count == 0)
            {
                throw new NotFoundException("Group with this name not found!");
            }
            return result;
        }
        public void Update(int id,Group group)
        {
            var result = _groupRepo.GetById(id);
            if (result == null)
            {
                throw new NotFoundException("Group with this id not found!");
            }
            if (!string.IsNullOrWhiteSpace(group.Name))
            {
                result.Name = group.Name;
            }

            if (!string.IsNullOrWhiteSpace(group.TeacherFullName))
            {
                result.TeacherFullName = group.TeacherFullName;
            }

            if (!string.IsNullOrWhiteSpace(group.RoomName))
            {
                result.RoomName = group.RoomName;
            }
        }
    }
}
