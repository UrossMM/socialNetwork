using socialNetwork.Models;
using socialNetwork.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Repository
{
    public interface IJWTManagerService
    {
        Tokens Authenticate(UserDTO user);
    }
}
