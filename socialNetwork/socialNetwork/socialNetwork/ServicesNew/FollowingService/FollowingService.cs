using Microsoft.AspNetCore.Http;
using socialNetwork.GenericRepo;
using socialNetwork.Models;
using socialNetwork.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace socialNetwork.Services
{
    public class FollowingService : IFollowingService
    {
        //private readonly AppDbContext _context;
        private readonly IGenericRepository<Following> _followingsRepository;

        public FollowingService(AppDbContext context, IGenericRepository<Following> repo)
        {
            //_context = context;
            _followingsRepository = repo;
        }

        public StatusError Follow(string user1, string user2)
        {
            // get by id
            // insert
            // delete
            // update
            // exists
            // get (filter, order by, include props)

            bool exists = _followingsRepository.Exists(f => f.FollowedId == user1 && f.FollowerId == user2);

            if (exists == false)
            {
                var newFollowing = new Following
                {
                    FollowedId = user1,
                    FollowerId = user2
                };
                _followingsRepository.Insert(newFollowing);

                try
                {
                    _followingsRepository.Save();
                    return new StatusError(StatusCodes.Status200OK, "Success");
                }
                catch 
                {
                    return new StatusError(StatusCodes.Status500InternalServerError, "ServerError");
                }
            }
            else
            {
                return new StatusError(StatusCodes.Status409Conflict, "Already exists");
            }
        }

        public List<UserDTO> MyFollowers(string myId)
        {
            //var joinRows = _context.Users.ToList().Join(
            //    _context.Followings,
            //    u => u.Id,
            //    f => f.FollowedId,
            //    (u, f) => new { FollowedId = f.FollowedId, FollowerId = f.FollowerId }
            //    ).ToList();

            //var filteredRows = joinRows.Where(f => f.FollowedId == myId).ToList();

            //var result = filteredRows.Join(
            //    _context.Users,
            //    f => f.FollowerId,
            //    u => u.Id,
            //    (f, u) => new UserDTO { Email = u.Email, Name = u.Name }
            //    ).ToList();

            var followers = _followingsRepository.Get(filter: x => x.FollowedId == myId, includeProperties: "Follower");

            List<UserDTO> result = followers
                .Select(x => new UserDTO
                {
                    Name = x.Follower.Name,
                })
                .ToList();  

            return result;
        }

        public List<UserDTO> WhoIFollow(string myId)
        {
            //var joinRows = _context.Users.ToList().Join(
            //   _context.Followings,
            //   u => u.Id,
            //   f => f.FollowerId,
            //   (u, f) => new { FollowedId = f.FollowedId, FollowerId = f.FollowerId }
            //   ).ToList();

            //var filteredRows = joinRows.Where(f => f.FollowerId == myId).ToList();

            //var result = filteredRows.Join(
            //    _context.Users,
            //    f => f.FollowedId,
            //    u => u.Id,
            //    (f, u) => new UserDTO { Email = u.Email, Name = u.Name }
            //    ).ToList();

            //return result;

            var following = _followingsRepository.Get(filter: x => x.FollowerId == myId, includeProperties: "Followed");

            List<UserDTO> result = following.Select(x => new UserDTO { Name = x.Followed.Name }).ToList();

            return result;
        }
    }
}
