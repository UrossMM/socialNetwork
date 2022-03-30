using socialNetwork.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Repositories
{
    public interface IFollowingRepo
    {
        List<UserDTO> MyFollowers(string myId);
        List<UserDTO> WhoIFollow(string myId);
        void Follow(string user1, string user2); //onaj ko pozove fju je onaj ko hoce da zaprati user1

    }
}
