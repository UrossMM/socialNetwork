using socialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace socialNetwork.ServicesNew.HttpServices
{
    public interface IHttpClientCodeMaze
    {
        void Post(HttpRequestBody request);
        void Get();
        Task<int> PostAsync(HttpRequestBody request);
        Task<U> PostJsonAsync<T, U>(T obj, string apiUrl, Dictionary<string, string> headers = null);
        Task<T> GetAsync<T>(string path, Dictionary<string, string> headers = null);
        Task<U> PutAsync<T, U>(string apiUrl, string param, T obj,  Dictionary<string, string> headers = null);
        Task<T> DeleteAsync<T>(string path, string param, Dictionary<string, string> headers = null);

       // Task<U> DefaultMethod<T, U>(string methodType, string path, Dictionary<string,string>headers = null, string param = null, T obj = default);
    }
}
