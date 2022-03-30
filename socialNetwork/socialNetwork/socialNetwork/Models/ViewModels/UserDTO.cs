using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Models.ViewModels
{
    public class UserDTO
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ApiKey { get; set; }

        /*
        public List<Group> Grupe { get; set; }
        public List<GroupUser> GroupUsers { get; set; }

        public List<Following> Followed { get; set; }
        public List<Following> Following { get; set; }
        public List<Post> Posts { get; set; }*/
    }
}
