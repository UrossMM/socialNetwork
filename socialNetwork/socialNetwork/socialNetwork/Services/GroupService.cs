using socialNetwork.Models;
using socialNetwork.Models.ViewModels;
using socialNetwork.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepo _groupRepo;

        public GroupService(IGroupRepo groupRepo)
        {
            _groupRepo = groupRepo;
        }

        public void AddUserToGroup(string id, int groupId)
        {
            _groupRepo.AddUserToGroup(id, groupId);
        }

        public List<GroupDTO> AllMyGroups(string myId)
        {
            return _groupRepo.AllMyGroups(myId);
        }

        public Group CreateGroup(Group group, string user)
        {
            return _groupRepo.CreateGroup(group, user);
        }

        public List<GroupDTO> GetAllGroups()
        {
            return _groupRepo.GetAllGroups();
        }

        public GroupDTO GetGroupById(int id)
        {
            return _groupRepo.GetGroupById(id);
        }

        public List<UserDTO> GroupMembers(int groupId)
        {
            return _groupRepo.GroupMembers(groupId);
        }

        public List<Group> GroupsWithInclude()
        {
            return _groupRepo.GroupsWithInclude();
        }

        public List<GroupDTO> MyGroups(string myId)
        {
            return _groupRepo.MyGroups(myId);
        }
        public List<UserDTO> AllAdmins()
        {
            return _groupRepo.AllAdmins();
        }
    }
}
