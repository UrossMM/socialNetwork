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

        public string GetKey(string email)
        {
            return _context.ApiKeyUsers.Where(a => a.Email == email).Select(a=> a.ApiKey).FirstOrDefault();
        }

        public void InsertKey(ApiKeyUser a)
        {
            _context.ApiKeyUsers.Add(a);
            _context.SaveChanges();
        }
    }
}
