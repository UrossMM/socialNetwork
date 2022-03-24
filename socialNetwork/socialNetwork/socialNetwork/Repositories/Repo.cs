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

        public List<string> GetPosts(int groupId, string user)
        {
            //sve objave iz jedne grupe
            var allRowsWithGroupId = _context.Posts.Where(p => p.GroupId == groupId).ToList();

            
            //iz odgovarajuce grupe izvucemo sve javne objave 
            var resultPosts = allRowsWithGroupId.Where(p => p.Type == "public").Select(p=> p.Content).ToList();

            //privatne objave da se filtriraju - treba da se proveri da li onaj ko hoce da vidi objave prati korisnike koji su napisali te privatne objave
            /*var filteredRows = allRowsWithGroupId.Where(p => p.Type == "privatna").Select(p => p.UserId).ToList();

            foreach(var k in filteredRows)
            {
                var checkFollowing =_context.Followings.Where(f => f.FollowedId == k && f.FollowerId == user).FirstOrDefault();
                if(checkFollowing != null)
                {
                    var correct = _context.Posts.Where(p => p.UserId == k).First();
                    resultPosts.Add(correct);
                    
                }
            }*/

            var joinRows = allRowsWithGroupId.Where(p => p.Type == "private").Join(

                _context.Followings,
                post => post.UserId,
                f => f.FollowedId,
                (post, f) => new PostDTO{Content= post.Content, FollowerId= f.FollowerId }).ToList();

            var filteredRows = joinRows.Where(p => p.FollowerId == user).Select(p=> p.Content).ToList();

            //takodje treba omoguciti da onaj ko zove fju vidi i svoje objave ( ukljuciti i svoje privatne objave!!!) jer ih prethodna naredba ne vraca jer
            // niko ne moze sam sebe da prati
            var myRows = allRowsWithGroupId.Where(p => p.Type == "private" && p.UserId == user).Select(p=>p.Content).ToList();

            resultPosts.AddRange(myRows);
            resultPosts.AddRange(filteredRows);
            /*foreach(var priv in filteredRows)
            {
                resultPosts.Add(priv);
            }*/

            return resultPosts;
        }
    }
}
