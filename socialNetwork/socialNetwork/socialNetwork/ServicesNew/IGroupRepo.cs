using socialNetwork.Models;
using socialNetwork.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Repositories
{
    public interface IGroupRepo
    {
        Group CreateGroup(Group group, string user); // group je grupa koju admin kreira, ili umesto USERVM int idUsera??
        List<GroupDTO> GetAllGroups();
        GroupDTO GetGroupById(int id);
        List<UserDTO> GroupMembers(int groupId);
        void AddUserToGroup(string id, int groupId); // user se dodaje u post od strane ADMINA(njegov id ne mora da se navodi? vec on samo poziva funkciju)
        List<GroupDTO> MyGroups(string myId); // vraca sve grupe koje je kreirao admin
        List<GroupDTO> AllMyGroups(string myId); //sve grupe u kojima sam clan
        List<UserDTO> AllAdmins();

        List<Group> GroupsWithInclude();
    }
}
