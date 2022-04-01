using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using socialNetwork.Attributes;
using socialNetwork.Models.ViewModels;
using socialNetwork.Repositories;
using socialNetwork.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Controllers
{
   // [ServiceFilter(typeof(ApiKeyFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class FollowingController : ControllerBase
    {
        private readonly IFollowingService _followingService;

        public FollowingController(IFollowingService followingController)
        {
            _followingService = followingController;
        }

        [HttpGet]
        [Route("getMyFollowers/{myId}")]
        public List<UserDTO> MyFollowers(string myId)
        {
            var result = _followingService.MyFollowers(myId);
            return result;
        }

        [HttpGet]
        [Route("whoIFollow/{myId}")]
        public List<UserDTO> WhoIFollow(string myId)
        {
            var result = _followingService.WhoIFollow(myId);
            return result;
        }

        [HttpPost]
        [Route("follow/{followed}/{follower}")]
        public IActionResult Follow(string followed, string follower)
        {
            _followingService.Follow(followed, follower);
            return Ok();
        }
    }
}
