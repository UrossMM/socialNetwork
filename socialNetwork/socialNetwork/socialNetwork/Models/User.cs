using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace socialNetwork.Models
{
    public class User : IdentityUser //svuda gde je bio IdentityUser menjamo sa User
    {
       // public int Id { get; set; }
        public string Name { get; set; }
       // public string Email { get; set; }
        public string Password { get; set; }

        public bool RememberMe { get; set; }
        public string ApiKey { get; set; }

        //referenca na grupe u kojima je admin
        //[JsonIgnore]

        public List<Group> Grupe { get; set; }
        [JsonIgnore]
        public List<GroupUser> GroupUsers { get; set; }
        //referenca na groupusers?? ili referenca na objave koje je pisao??

        //referenca na followed i follower
        [JsonIgnore]
        public List<Following> Followed { get; set; }
        [JsonIgnore]
        public List<Following> Following { get; set; }
        [JsonIgnore]
        public List<Post> Posts { get; set; }
    }
}
