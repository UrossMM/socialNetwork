using socialNetwork.Models;
using socialNetwork.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Repositories
{
    public class FollowingRepo : IFollowingRepo
    {
        private readonly AppDbContext _context;

        public FollowingRepo(AppDbContext context)
        {
            _context = context;
        }

        public void Follow(string user1, string user2)
        {
            // provera da li user2 vec ne prati user1 
            var checkFollowing = _context.Followings.Where(f => f.FollowedId == user1 && f.FollowerId == user2).FirstOrDefault();

            if (checkFollowing == null)
            {
                var newFollowing = new Following
                {
                    FollowedId = user1,
                    FollowerId = user2
                };

                _context.Followings.Add(newFollowing);
                _context.SaveChanges();
            }
        }

        public List<UserDTO> MyFollowers(string myId)
        {
            var joinRows = _context.Users.ToList().Join(
                _context.Followings,
                u => u.Id,
                f => f.FollowedId,
                (u, f) => new { FollowedId = f.FollowedId, FollowerId = f.FollowerId }
                ).ToList();

            var filteredRows = joinRows.Where(f => f.FollowedId == myId).ToList();

            var result = filteredRows.Join(
                _context.Users,
                f => f.FollowerId,
                u => u.Id,
                (f, u) => new UserDTO { Email = u.Email, Name = u.Name }
                ).ToList();

            return result;
        }

        public List<UserDTO> WhoIFollow(string myId)
        {
            var joinRows = _context.Users.ToList().Join(
               _context.Followings,
               u => u.Id,
               f => f.FollowerId,
               (u, f) => new { FollowedId = f.FollowedId, FollowerId = f.FollowerId }
               ).ToList();

            var filteredRows = joinRows.Where(f => f.FollowerId == myId).ToList();

            var result = filteredRows.Join(
                _context.Users,
                f => f.FollowedId,
                u => u.Id,
                (f, u) => new UserDTO { Email = u.Email, Name = u.Name }
                ).ToList();

            return result;
        }
    }
}
