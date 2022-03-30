using socialNetwork.Models.ViewModels;
using socialNetwork.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Services
{
    public interface IFollowingService
    {
        List<UserDTO> MyFollowers(string myId);
        List<UserDTO> WhoIFollow(string myId);
        void Follow(string user1, string user2);
    }
}
