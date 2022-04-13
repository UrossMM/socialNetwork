using Newtonsoft.Json;
using socialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace socialNetwork.ServicesNew
{
    public class HttpClientFactoryService : IHttpClientService
    {

        private readonly HttpClient _httpClient;

        public HttpClientFactoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public void Get()
        {
            throw new NotImplementedException();
        }

        public Task GetAsync()
        {
            throw new NotImplementedException();
        }

        public void Post(HttpRequestBody request)
        {
            
        }

        public async Task<int> PostAsync(HttpRequestBody request)
        {
            //dobija podatke
            var stringRequest = JsonConvert.SerializeObject(request);

            //napravi request
           // HttpContent c = new StringContent(stringRequest, Encoding.UTF8, "application/json");

            //vrati rezultat
            var result = await _httpClient.PostAsJsonAsync("http://localhost:2571/", request);
           
            return (int)result.StatusCode;
        }

    
    }
}
