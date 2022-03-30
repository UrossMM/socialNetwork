using socialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Repositories
{
    public interface IApiKeyRepo
    {
        string GetKey(string email);
        void InsertKey(ApiKeyUser a);
        List<string> GetAllKeys();

    }
}
