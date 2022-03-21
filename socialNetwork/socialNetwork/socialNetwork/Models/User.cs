using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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
        //public List<Grupa> Grupe { get; set; }
    }
}
