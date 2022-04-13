using AutoMapper;
using Microsoft.EntityFrameworkCore;
using socialNetwork.Models;
using socialNetwork.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Repositories
{
    public class GroupRepo : IGroupRepo
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GroupRepo(AppDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

     

        public void AddUserToGroup(string id, int groupId)
        {
            //provera da li taj user vec nije u toj grupi
            var checkMembership = _context.GroupUsers.Where(f => f.UserId == id && f.GroupId == groupId).FirstOrDefault();

            if (checkMembership == null)
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
                (gu, g) => new GroupDTO { Name = g.Name, AdminId = g.AdminId } //vraca sve grupe (vraca adminid a kad probam ceo objekat tad baca onu gresku za cycle)
                ).ToList();

            return result;
        }

        public Group CreateGroup(Group group, string user)
        {
            //samo jedna grupa sa jednim imenom
            var existingName = _context.Groups.FirstOrDefault(g => g.Name == group.Name);
            if (existingName == null)
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

        public List<GroupDTO> GetAllGroups()
        {
            var result = _context.Groups.Select(g => new GroupDTO { Name = g.Name, AdminId = g.AdminId }).ToList();
            return result;
        }

        public GroupDTO GetGroupById(int id)
        {
            var result = _context.Groups.Where(g => g.Id == id).Select(g => new GroupDTO { Name = g.Name, AdminId = g.AdminId }).FirstOrDefault();
            //.FirstOrDefault(g => g.Id == id); ovo je bilo umesto where i select dok je Group bio povratni tip
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

            bool empty = string.IsNullOrEmpty("asdf");

            empty = "asdf".IsNullOrEmpty();

            return result;
        }

        public List<Group> GroupsWithInclude()
        {
            var includegroup = _context.Groups.Include(x => x.Admin).ToList();
            return includegroup;
        }

        public List<GroupDTO> MyGroups(string myId)
        {
            var admin = _context.Users.Find(myId);
            var groups = _context.Groups.Where(g => g.AdminId == myId).Select(g => new GroupDTO { Name = g.Name, AdminId = g.AdminId }).ToList();
            return groups;
        }

        public List<UserDTO> AllAdmins()
        {
            var admins = _context.Groups.Include(x => x.Admin).Select(x => x.Admin).Distinct().ToList();
            List<UserDTO> res = _mapper.Map<List<UserDTO>>(admins);
            return res;
        }
    }

    public static class StringExtensions
    {
        public static int WordCount(this string str)
        {
            return str.Split(new char[] { ' ', '.', '?' },
                             StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
    }
}
