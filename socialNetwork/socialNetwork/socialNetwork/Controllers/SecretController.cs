using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using socialNetwork.Attributes;
using socialNetwork.Models.ViewModels;
using socialNetwork.Paging;
using socialNetwork.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(ApiKeyFilter))]
    public class SecretController : ControllerBase
    {
        private readonly IPostService _postService;

        [HttpPost]
        [Route("getPosts/{groupId}/{user}")]
        public List<PostDTO> GetPosts(int groupId, string user, PagingProperties prop)
        {
            var result = _postService.GetPosts(groupId, user, prop);
            return result;
        }
    }
}
