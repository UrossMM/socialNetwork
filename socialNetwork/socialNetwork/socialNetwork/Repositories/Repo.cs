using socialNetwork.Models;
using socialNetwork.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Repositories
{
    public class Repo : IRepo
    {

        private readonly AppDbContext _context;

        public Repo(AppDbContext context)
        {
            _context = context;
        }

        public void AddUserToGroup(string id, Group group)
        {
            var groupUser = new GroupUser()
            {
                UserId = id,
                GroupId = group.Id
            };

            _context.GroupUsers.Add(groupUser);
            _context.SaveChanges();
        }

        public void CommentPost(Comment comm, Post post)
        {
            throw new NotImplementedException();
        }

        public void CreateGroup(Group group, string user)
        {
            //samo jedna grupa sa jednim imenom
            var existingName = _context.Groups.FirstOrDefault(g => g.Name == group.Name);
            if(existingName == null)
            {
                var newGroup = new Group { Name = group.Name, AdminId = user };
                //var adminGrupe = _context.Users.Find( user);
                // adminGrupe.Grupe.Add(newGroup);
                _context.Groups.Add(newGroup);
                _context.SaveChanges();

                //Ako admin napravi grupu onda se on automatski i doda u nju

                //izvuci id dodate grupe (koji se generise sam pri dodavanju)
                newGroup = _context.Groups.FirstOrDefault(g => g.Name == group.Name);

                var groupUser = new GroupUser()
                {
                    UserId = user,
                    GroupId = newGroup.Id
                };

                _context.GroupUsers.Add(groupUser);
                _context.SaveChanges();
            }

            

        }

        public List<Group> MyGroups(string myId)
        {
            var admin = _context.Users.Find(myId);
            var groups = _context.Groups.Where(g => g.AdminId == myId).ToList();
            return groups;
        }
        public List<Group> GetAllGroups()
        {
            var result = _context.Groups.ToList();
            return result;
        }
        public Group GetGroupById(int id)
        {
            var result = _context.Groups.FirstOrDefault(g => g.Id == id);
            return result;
        }

        public void CreatePost(Post post, Group group, UserDTO user)
        {
            throw new NotImplementedException();
        }

        public void Follow(UserDTO user1, UserDTO user2)
        {
            var newFollowing = new Following
            {
                FollowedId = user1.Id,
                FollowerId = user2.Id
            };
        }

        public void GetAllPosts(Group group)
        {
            throw new NotImplementedException();
        }
    }
}
