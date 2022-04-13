using socialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.ServicesNew.HttpServices
{
    public interface IHttpClientRestSharpService
    {

        void Post(HttpRequestBody request);
        void Get();
        Task<int> PostAsync(HttpRequestBody request);
        Task GetAsync();
    }
}
