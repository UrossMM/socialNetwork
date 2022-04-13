using socialNetwork.Models;
using socialNetwork.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Services
{
    public interface IGroupService
    {
        Group CreateGroup(Group group, string user); 
        List<GroupDTO> GetAllGroups();
        GroupDTO GetGroupById(int id);
        List<UserDTO> GroupMembers(int groupId);
        void AddUserToGroup(string id, int groupId); 
        List<GroupDTO> MyGroups(string myId); 
        List<GroupDTO> AllMyGroups(string myId); 
        List<Group> GroupsWithInclude();
        List<UserDTO> AllAdmins();

    }
}
