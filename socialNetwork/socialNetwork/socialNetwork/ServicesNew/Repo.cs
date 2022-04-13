using socialNetwork.Models;
using socialNetwork.Models.ViewModels;
using socialNetwork.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Repositories
{
    public class Repo : IRepo
    {

        private readonly AppDbContext _context;
        private const int PageSize = 3;

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

        public List<GroupDTO> MyGroups(string myId)
        {
            var admin = _context.Users.Find(myId);
            var groups = _context.Groups.Where(g => g.AdminId == myId).Select(g=> new GroupDTO { Name = g.Name, AdminId = g.AdminId }).ToList();
            return groups;
        }
        public List<GroupDTO> GetAllGroups()
        {
            var result = _context.Groups.Select(g=> new GroupDTO {Name=g.Name, AdminId=g.AdminId }).ToList();
            return result;
        }
        public GroupDTO GetGroupById(int id)
        {
            var result = _context.Groups.Where(g => g.Id == id).Select(g => new GroupDTO { Name = g.Name, AdminId = g.AdminId }).FirstOrDefault();
                //.FirstOrDefault(g => g.Id == id); ovo je bilo umesto where i select dok je Group bio povratni tip
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

        public List<string> GetPosts(int groupId, string user, int? pageNumber)
        {
            //sve objave iz jedne grupe
            var allRowsWithGroupId = _context.Posts.Where(p => p.GroupId == groupId).ToList();

            //ako je admin grupe pozvao funkciju on vidi sve objave cak iako ne prati korisnika koji je napisao privatnu objavu!!!!!!!
            var isAdmin = _context.Groups.FirstOrDefault(g => g.AdminId == user && g.Id==groupId);
            if(isAdmin != null)
            {
                return PaginatedList<string>.Create(allRowsWithGroupId.Select(g => g.Content).ToList().AsQueryable(), pageNumber ?? 1, PageSize);
               // return allRowsWithGroupId.Select(g=> g.Content).ToList();
            }
            else
            {
                //iz odgovarajuce grupe izvucemo sve javne objave 
                var resultPosts = allRowsWithGroupId.Where(p => p.Type == "public").Select(p => p.Content).ToList();

                //privatne objave da se filtriraju - treba da se proveri da li onaj ko hoce da vidi objave prati korisnike koji su napisali te privatne objave
                
                var joinRows = allRowsWithGroupId.Where(p => p.Type == "private").Join(

                    _context.Followings,
                    post => post.UserId,
                    f => f.FollowedId,
                    (post, f) => new { Content = post.Content, FollowerId = f.FollowerId }).ToList();

                var filteredRows = joinRows.Where(p => p.FollowerId == user).Select(p => p.Content).ToList();

                //takodje treba omoguciti da onaj ko zove fju vidi i svoje objave ( ukljuciti i svoje privatne objave!!!) jer ih prethodna naredba ne vraca jer
                // niko ne moze sam sebe da prati
                var myRows = allRowsWithGroupId.Where(p => p.Type == "private" && p.UserId == user).Select(p => p.Content).ToList();

                resultPosts.AddRange(myRows);
                resultPosts.AddRange(filteredRows);

                resultPosts = PaginatedList<string>.Create(resultPosts.AsQueryable(), pageNumber ?? 1, PageSize);

                return resultPosts;
            }
            
            
        }

        public List<UserDTO> MyFollowers(string myId)
        {
            var joinRows =_context.Users.ToList().Join(
                _context.Followings,
                u => u.Id,
                f => f.FollowedId,
                (u, f) => new  {  FollowedId = f.FollowedId ,FollowerId = f.FollowerId }
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

        public List<GroupDTO> AllMyGroups(string myId)
        {
            var joinRows = _context.Users.ToList().Join(
                _context.GroupUsers,
                u => u.Id,
                gu => gu.UserId,
                (u, gu) => new { UserId = u.Id, Groupid = gu.GroupId }
                ).ToList(); 

            var filteredRows = joinRows.Where(gu => gu.UserId == myId).ToList();

            var result = filteredRows.Join(
                _context.Groups,
                gu => gu.Groupid,
                g => g.Id,
                (gu, g) => new GroupDTO { Name = g.Name, AdminId= g.AdminId } //vraca sve grupe (vraca adminid a kad probam ceo objekat tad baca onu gresku za cycle)
                ).ToList();

            return result;
        }

        public List<UserDTO> GroupMembers(int groupId)
        {
            var joinRows = _context.Groups.ToList().Join(
                _context.GroupUsers,
                g => g.Id,
                gu => gu.GroupId,
                (g, gu) => new { GroupId = g.Id, UserId = gu.UserId }
                ).ToList();

            var filteredRows = joinRows.Where(gu => gu.GroupId == groupId).ToList();

            var result = filteredRows.Join(
                _context.Users,
                gu => gu.UserId,
                u => u.Id,
                (gu, u) => new UserDTO { Email = u.Email, Name = u.Name/*, Grupe=u.Grupe*/} //za Grupe baca gresku za cycle
                ).ToList();
              
            return result;

        }

        public List<CommentDTO> PostComments(int postId)
        {
            var result = _context.Comments.Where(c => c.PostId == postId && c.ParentId == null).Select(c=> new CommentDTO
            {
                Text = c.Text,
            }).ToList();
            
            return result;
        }

        public List<CommentDTO> CommentComments(int commId)
        {
            var result = _context.Comments.Where(c => c.ParentId == commId).Select(c => new CommentDTO
            {
                Text = c.Text,
            }).ToList();

            return result;
        }
    }
}
