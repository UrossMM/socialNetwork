
using socialNetwork.Models;
using socialNetwork.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Services
{
    public class ApiKeyService : IApiKeyService
    {
        private readonly IApiKeyRepo _apiKeyRepo;

        public ApiKeyService(IApiKeyRepo apiKeyRepo)
        {
            _apiKeyRepo = apiKeyRepo;
        }

        public List<string> GetAllKeys()
        {
            return _apiKeyRepo.GetAllKeys();
        }

        public bool GetKey(string email)
        {
            return _apiKeyRepo.GetKey(email);
        }

        public string GetMyKey(string email)
        {
            return _apiKeyRepo.GetMyKey(email);
        }

        public void InsertKey(ApiKeyUser a)
        {
            _apiKeyRepo.InsertKey(a);
        }
    }
}
