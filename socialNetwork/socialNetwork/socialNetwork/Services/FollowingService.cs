using socialNetwork.Models.ViewModels;
using socialNetwork.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Services
{
    public class FollowingService : IFollowingService
    {
        private readonly IFollowingRepo _followingRepo;

        public FollowingService(IFollowingRepo followingRepo)
        {
            _followingRepo = followingRepo;
        }

        public void Follow(string user1, string user2)
        {
            _followingRepo.Follow(user1, user2);
        }

        public List<UserDTO> MyFollowers(string myId)
        {
            return _followingRepo.MyFollowers(myId);
        }

        public List<UserDTO> WhoIFollow(string myId)
        {
            return _followingRepo.WhoIFollow(myId);
        }
    }
}
