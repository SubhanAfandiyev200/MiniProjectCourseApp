using Domain.Models;
using Repository.Repositories.Interfaces;
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

        public void Create(Group group)
        {
            _groupRepo.Create(group);
        }
    }
}
