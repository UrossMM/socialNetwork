using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using socialNetwork.Models;
using socialNetwork.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Attributes
{
    //[AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyFilter : IAsyncActionFilter
    {
        private const string APIKEYNAME = "ApiKey";
        private readonly IApiKeyService _apiKeyService;

        public ApiKeyFilter(IApiKeyService apiKeyService)
        {
            _apiKeyService = apiKeyService;
        }

        // private readonly UserManager<User> userManager;

        /*public ApiKeyAttribute(UserManager<User> userManager)
        {
            //this.userManager = userManager;
        }*/

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {  
            if (!context.HttpContext.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))//checking the request headers collection object if it has a key with name ApiKey. 
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Api Key was not provided"
                };
                //context.HttpContext.Request.Body
                return;
            }

            //var apiKeyService = context.HttpContext.RequestServices.GetRequiredService<IApiKeyService>();
            //var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            //ovde da se uporedi sa apikljucem korisnika 
            var apiKey = _apiKeyService.GetKey(extractedApiKey);
            if(!apiKey)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Unauthorized client"
                };
                return;
            }
            /*bool isFound = false;

            foreach (var apiKey in apiKeys)
            {
                if (apiKey.Equals(extractedApiKey) && isFound==false)
                {
                    isFound = true;
                }
            }*/

            /*if(isFound == false)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Unauthorized client"
                };
                return;
            }*/

           /* var apiKey = appSettings.GetValue<string>(APIKEYNAME);

            if (!apiKey.Equals(extractedApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Unauthorized client"
                };
                return;
            }*/

            //poziv dolazi pre next i obradjuje se 

            await next();

           
        }
    }
}
