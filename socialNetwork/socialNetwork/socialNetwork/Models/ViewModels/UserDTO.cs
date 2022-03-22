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
        //public string ConfirmPassword { get; set; }

    }
}
