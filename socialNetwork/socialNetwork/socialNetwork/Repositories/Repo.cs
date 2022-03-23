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

        public void AddUserToGroup(string id, int groupId)
        {
            //provera da li taj user vec nije u toj grupi
            var checkMembership = _context.GroupUsers.Where(f => f.UserId == id && f.GroupId == groupId).FirstOrDefault();

            if(checkMembership == null)
            {
                var groupUser = new GroupUser()
                {
                    UserId = id,
                    GroupId = groupId
                };

                _context.GroupUsers.Add(groupUser);
                _context.SaveChanges();
            }
            
        }

        public Comment AddComment(Comment newComm, int postId, int? parentComm = null)
        {
            var newComment = new Comment
            {
                Text = newComm.Text,
                PostId = postId,
                ParentId= parentComm
            };

            _context.Comments.Add(newComment);
            _context.SaveChanges();

            return newComment;
        }

        public Group CreateGroup(Group group, string user)
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

                return newGroup;
            }

            return null;

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

        public Post CreatePost(Post post, int groupId, string userId)
        {

            //da li je osoba koja hoce da doda post u grupi clan te grupe
            var member = _context.GroupUsers.Where(c => c.UserId == userId && c.GroupId==groupId).FirstOrDefault();

            if(member!=null)
            {
                var newPost = new Post
                {
                    Content = post.Content,
                    Type = post.Type,
                    GroupId = groupId,
                    UserId = userId
                };

                _context.Posts.Add(newPost);
                _context.SaveChanges();

                return newPost;
            }

            return null;
        }

        public void Follow(string user1, string user2)
        {
            // provera da li user2 vec ne prati user1 
            var checkFollowing = _context.Followings.Where(f => f.FollowedId == user1 && f.FollowerId == user2).FirstOrDefault();

            if(checkFollowing == null)
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

        public void GetAllPosts(int groupId)
        {
            throw new NotImplementedException();
        }
    }
}
