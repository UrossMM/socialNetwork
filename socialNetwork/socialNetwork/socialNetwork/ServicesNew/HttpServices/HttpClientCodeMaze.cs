using Newtonsoft.Json;
using socialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace socialNetwork.ServicesNew.HttpServices
{
    public class HttpClientCodeMaze : IHttpClientCodeMaze
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _options;
        private string _apiUrl;
       // private Dictionary<string, string> _authorization;
        public HttpClientCodeMaze(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
           // _apiUrl = "http://localhost:2571/";
            //_authorization.Add("Bearer", "TestToken");
            //_authorization.Add("ApiKey", "TestKey");
        }
        public void Get()
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetAsync<T>(string path, Dictionary<string, string> headers = null)
        {
            var httpClient = _httpClientFactory.CreateClient();

            /*httpClient.BaseAddress = new Uri(apiUrl);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));*/
            // httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", _authorization["Bearer"]);

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(path)
            };

            foreach(var header in headers)
            {
                httpRequestMessage.Headers.Add(header.Key, header.Value);
            }

            var response = await httpClient.SendAsync(httpRequestMessage);
            //var response = await httpClient.GetStringAsync(path);

            /*Type type = typeof(T);
            var typeInstance = Activator.CreateInstance(type);*/

            try
            {
                string result = "";
                if (response.Content != null)
                    result = await response.Content.ReadAsStringAsync();
                return  JsonConvert.DeserializeObject<T>(result);
            }
            catch (Exception)
            {
                return default(T);
            }

            //return (T)typeInstance;
        }

        public void Post(HttpRequestBody request)
        {
            throw new NotImplementedException();
        }

        public async Task<int> PostAsync(HttpRequestBody request)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", "ApiKey");

            using (var result = await httpClient.PostAsJsonAsync("http://localhost:2571/", request))
            {
                result.EnsureSuccessStatusCode();
                var stream = await result.Content.ReadAsStreamAsync();
                //var companies = await JsonSerializer.DeserializeAsync<List<CompanyDto>>(stream, _options);
                return (int)result.StatusCode;
            }
        }

        
        public async Task<U> PostJsonAsync<T, U>(T obj, string apiUrl, Dictionary<string, string> headers = null)
        {
            _apiUrl = apiUrl;

            var httpClient = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(apiUrl),
                Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
            };
           
            foreach (var d in headers)
            {
                httpRequestMessage.Headers.Add(d.Key, d.Value);
            }

            var response = await httpClient.SendAsync(httpRequestMessage);
            
            //Type type = typeof(U);
           // var typeInstance = Activator.CreateInstance(type);

            try
            {
                string result = "";
                if (response.Content != null)
                    result = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<U>(result);
            }
            catch (Exception)
            {
                return default(U);
            }

            
        }
        /*
        public async Task<T> PatchAsync<T>(string apiUrl, Dictionary<string, string> headers = null)
        {
            /*var httpClient = _httpClientFactory.CreateClient();

            var httpResuestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Patch,
                RequestUri = new Uri(apiUrl),
                //Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
            };
           
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await httpClient.PatchAsync(path, content);
            string result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(result);
        }
    */
        public async Task<T> DeleteAsync<T>(string path, string param, Dictionary<string, string> headers = null)
        {
            //param da se prosledi u path ili u content?
            var httpClient = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(path),
                Content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json")
            };

            foreach (var d in headers)
            {
                httpRequestMessage.Headers.Add(d.Key, d.Value);
            }

            var response = await httpClient.SendAsync(httpRequestMessage);
            

            try
            {
                string result = "";
                if(response.Content!=null)
                    result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);
            }
            catch (Exception)
            {
                return default(T);
            } 
        }



        /*public Task DefaultMethod(string type, string path)
        {
            throw new NotImplementedException();
        }*/

        public async Task<U> PutAsync<T, U>(string apiUrl, string param, T obj, Dictionary<string, string> headers = null)
        { // ko poziva metodu on ce param (kriterijum po kome se brise) da prosledi kao query ili route u apiUrl(vec ce to da pripremi) tako da nam
            //param ne treba ovde?
            var httpClient = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(apiUrl),
                Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
            };

            foreach (var d in headers)
            {
                httpRequestMessage.Headers.Add(d.Key, d.Value);
            }

            var response = await httpClient.SendAsync(httpRequestMessage);

            try
            {
                string result = "";
                if (response.Content != null)
                    result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<U>(result);
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}
