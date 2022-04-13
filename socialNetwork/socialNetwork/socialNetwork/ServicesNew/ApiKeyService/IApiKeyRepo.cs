using socialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Repositories
{
    public interface IApiKeyRepo
    {
        bool GetKey(string email);
        void InsertKey(ApiKeyUser a);
        List<string> GetAllKeys();
        string GetMyKey(string email);
    }
}
