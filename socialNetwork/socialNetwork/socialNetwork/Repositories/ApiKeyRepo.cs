using socialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Repositories
{
    public class ApiKeyRepo : IApiKeyRepo
    {
        private readonly AppDbContext _context;

        public ApiKeyRepo(AppDbContext context)
        {
            _context = context;
        }

        public List<string> GetAllKeys()
        {
            return _context.ApiKeyUsers.Select(a => a.ApiKey).ToList();
        }

        public bool GetKey(string key)
        {
            return _context.ApiKeyUsers.Any(k=> k.ApiKey ==key);
        }

        public void InsertKey(ApiKeyUser a)
        {
            _context.ApiKeyUsers.Add(a);
            _context.SaveChanges();
        }
    }
}
