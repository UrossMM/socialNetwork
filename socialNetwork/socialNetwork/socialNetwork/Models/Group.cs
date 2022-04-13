using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace socialNetwork.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //referenca na admina grupe (User)
        public string AdminId { get; set; }
        public User Admin { get; set; }

        //referenca na tabelu GroupUser??? -za sad da
        [JsonIgnore]

        public List<GroupUser> GroupUsers { get; set; }
        [JsonIgnore]

        public List<Post> Posts { get; set; }

        /*referenca na tabelu Post
        public List<Post> Posts { get; set; }*/
    }
}
