using RestSharp;
using socialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace socialNetwork.ServicesNew.HttpServices
{
    public class HttpClientRestSharpService : IHttpClientRestSharpService
    {
        private readonly RestClient _restClient;
        private readonly string _url = "http://localhost:2571/";

       

        public HttpClientRestSharpService()
        {
            _restClient = new RestClient(_url);
        }
        public void Get()
        {

        }
        public async Task GetAsync()
        {
            var req = new RestRequest("post", Method.Get);
            string uri = "endpoint";
            var args = new
            {
                id = "123",
                foo = "bar"
            };
            var res = await _restClient.GetJsonAsync<HttpRequestBody>("endpoint/{id}", args );

            
        }

        

        public void Post(HttpRequestBody request)
        {
            throw new NotImplementedException();
        }

        public async Task<int> PostAsync(HttpRequestBody request)
        {
            var req = new RestRequest("post", Method.Post)
                //.AddQueryParameter("ime", "Uros")
                .AddJsonBody(request);
            var res = await _restClient.PostAsync(req);
            
            return (int)res.StatusCode;
        }
    }
}
