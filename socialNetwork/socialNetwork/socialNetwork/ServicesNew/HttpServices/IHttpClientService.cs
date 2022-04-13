using socialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.ServicesNew
{
    public interface IHttpClientService
    {
        void Post(HttpRequestBody request);
        void Get();
        Task<int> PostAsync(HttpRequestBody request);
        Task GetAsync();
    }
}
