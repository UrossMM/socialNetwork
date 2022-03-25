using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace socialNetwork.Models.ViewModels
{
    public class GroupDTO
    {
        public string Name { get; set; }
        public string AdminId { get; set; }
        public User Admin { get; set; }

        //public List<GroupUser> GroupUsers { get; set; }
       // public List<Post> Posts { get; set; }
    }
}
