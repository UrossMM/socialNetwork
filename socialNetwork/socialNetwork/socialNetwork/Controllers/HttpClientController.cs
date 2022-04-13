using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using socialNetwork.Models;
using socialNetwork.ServicesNew;
using socialNetwork.ServicesNew.HttpServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace socialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HttpClientController : ControllerBase
    {

        private readonly IHttpClientService _httpClientService;
        private readonly IHttpClientRestSharpService _httpClientRestSharpService;
        private readonly IHttpClientCodeMaze _httpClientCodeMaze;

        public HttpClientController(IHttpClientService httpClientService, IHttpClientRestSharpService httpClientRestSharpService, IHttpClientCodeMaze httpClientCodeMaze) 
        {
            _httpClientService = httpClientService;
            _httpClientRestSharpService = httpClientRestSharpService;
            _httpClientCodeMaze = httpClientCodeMaze;
        }

        [HttpPost]
        [Route("testClientFactory")]
        public async Task<IActionResult> TestClientFactory()
        {
            HttpRequestBody request = new()
            {
                Broj = 22,
                Code = 545,
                Ime = "asdf"
            };

            var result = await _httpClientService.PostAsync(request);
            return StatusCode(result);
        }

        [HttpPost]
        [Route("testClientRestSharp")]
        public async Task<IActionResult> TestClientRest()
        {
            HttpRequestBody request = new()
            {
                Broj = 99,
                Code = 8888,
                Ime = "Pegi"
            };

            

            var result = await _httpClientRestSharpService.PostAsync(request);
            return StatusCode(result);
        }

        [HttpPost]
        [Route("testClientPost")]
        public async Task<IActionResult> TestClientCodeMaze()
        {
            HttpRequestBody request = new()
            {
                Broj = 146,
                Code = 679,
                Ime = "Nadja"
            };

            Dictionary<string, string> headers = new Dictionary<string, string>() 
            {
                { "Accept", "application/json" },
                { "X-Version", "1" },
                { "Y-Version", "2" },
                { "Z-Version", "3" }
            };

            string apiUrl = "http://localhost:2571/";

            var result = await _httpClientCodeMaze.PostJsonAsync<HttpRequestBody, HttpResponseBody>(request, apiUrl, headers);

            return Ok(result);
            //return StatusCode(StatusCodes.Status200OK);
        }

        [HttpGet]
        [Route("testClientGet")]
        public async Task<IActionResult> TestClientGet()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { HeaderKeys.Authorization, "1" },
                { HeaderKeys.Token, "2" }
            };

            string apiUrl = "http://localhost:2571/";

            var result = await _httpClientCodeMaze.GetAsync<HttpResponseBody>(apiUrl, headers);

            return Ok(result);
        }

        [HttpDelete]
        [Route("testClientDelete")]
        public async Task<IActionResult> TestClientDelete([FromQuery]string apiUrl, [FromBody]Dictionary<string, string> headers)
        {
            /*Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { HeaderKeys.Authorization, "1" },
                { HeaderKeys.Token, "2" }
            };*/

            //string apiUrl = "http://localhost:2571/";
            string param = "uros";

            var result = await _httpClientCodeMaze.DeleteAsync<HttpResponseBody>(apiUrl, param, headers);

            return Ok(result);
        }

        [HttpPut]
        [Route("testClientPut")]
        public async Task<IActionResult> TestClientPut()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { HeaderKeys.Authorization, "1" },
                { HeaderKeys.Token, "2" }
            };

            string apiUrl = "http://localhost:2571/";

            string param = "uros";

            HttpRequestBody request = new()
            {
                Broj = 146,
                Code = 679,
                Ime = "Nadja"
            };

            var result = await _httpClientCodeMaze.PutAsync<HttpRequestBody , HttpResponseBody>(apiUrl, param, request, headers);

            return Ok(result);
        }
    }
}
