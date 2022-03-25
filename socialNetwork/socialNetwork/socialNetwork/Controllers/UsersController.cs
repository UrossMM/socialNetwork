using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using socialNetwork.Models;
using socialNetwork.Models.ViewModels;
using socialNetwork.Repositories;
using socialNetwork.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IJWTManagerService tokenService;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private IRepo repository;
        private readonly IMapper _mapper;
        public UsersController(IJWTManagerService repository, UserManager<User> userManager, SignInManager<User> signInManager, IRepo repo, IMapper mapper)
        {
            tokenService = repository;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.repository = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public List<string> Get()
        {
            var users = new List<string> 
            {
                "Satinder singh",
                "Amit Sarna",
                "David John"
            };

            return users;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> LogIn([FromBody]UserDTO userdata)
        {
            var user = new User { UserName = userdata.Email, Email = userdata.Email };
            await signInManager.SignInAsync(user, isPersistent: false);
            var token = tokenService.Authenticate(userdata);

            if (token == null)
            {
               return Unauthorized();
            }

               return Ok(token);
                       
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]UserDTO model)
        {
            var user = new User { UserName = model.Email, Email = model.Email, RememberMe = model.RememberMe, Name = model.Name };
            var result = await userManager.CreateAsync(user, model.Password);

            //await signInManager.SignInAsync(user, isPersistent: false);
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("createGroup")]
        public async Task<IActionResult> CreateGroup(Group group, string user)
        {
            var result = repository.CreateGroup(group, user);
            return Ok(result);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("getAllGroups")]
        public async Task<IActionResult> GetAllGroups()
        {
            return Ok(repository.GetAllGroups());

        }

        [HttpPost]
        [AllowAnonymous]
        [Route("getGroupById/{id}")]
        public async Task<IActionResult> GetGroupById(int id)
        {
            return Ok(repository.GetGroupById(id));

        }

        [HttpPost]
        [AllowAnonymous]
        [Route("getAdminGroups/{id}")]
        public IActionResult GetAdminGroups(string id)
        {
            var result = repository.MyGroups(id);
            List<GroupDTO> res = _mapper.Map<List<GroupDTO>>(result);
            return Ok(res);

        }

        [HttpPost]
        [AllowAnonymous]
        [Route("addUserToGroup/{id}/{groupId}")]
        public IActionResult AddUserToGroup(string id, int groupId)
        {
            repository.AddUserToGroup(id, groupId);
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("createPost/{groupId}/{userId}")]
        public IActionResult CreatePost(Post post, int groupId, string userId)
        {
            var result = repository.CreatePost(post, groupId, userId);
            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("follow/{followed}/{follower}")]
        public IActionResult Follow(string followed, string follower)
        {
            repository.Follow(followed, follower);
            return Ok();
        }


        //radi iz postmana kada se pozove bez parentComm, a iz swaggera ne
        /*[HttpPost]
        [AllowAnonymous]
        [Route("addComment/{postId}/{parentComm?}")]
        public IActionResult AddComment(Comment newComm, int postId, int? parentComm = null)
        {
            repository.AddComment(newComm, postId, parentComm);
            return Ok();
        }*/

       [HttpPost]
       [AllowAnonymous]
       [Route("addComment/{postId}")]
       public IActionResult AddComment(Comment newComm, int postId, [FromQuery]int? parentComm)
       {
           repository.AddComment(newComm, postId, parentComm);
           return Ok();
       }

        [HttpGet]
        [AllowAnonymous]
        [Route("getPosts/{groupId}/{user}")]
        public List<string> GetPosts(int groupId, string user, [FromQuery]int pageNumber)
        {
            var result = repository.GetPosts(groupId, user, pageNumber);
            return result;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("getMyFollowers/{myId}")]
        public List<UserDTO> MyFollowers(string myId)
        {
            var result = repository.MyFollowers(myId);
            return result;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("whoIFollow/{myId}")]
        public List<UserDTO> WhoIFollow(string myId)
        {
            var result = repository.WhoIFollow(myId);
            return result;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("allMyGroups/{myId}")]
        public List<GroupDTO> AllMyGroups(string myId)
        {
            var result = repository.AllMyGroups(myId);
            return result;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("allMembersInGroup/{groupId}")]
        public List<UserDTO> GroupMembers(int groupId)
        {
            var result = repository.GroupMembers(groupId);
            return result;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("postComments/{postId}")]
        public List<CommentDTO> PostComments(int postId)
        {
            var result = repository.PostComments(postId);
            return result;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("commentComments/{commId}")]
        public List<CommentDTO> CommentComments(int commId)
        {
            var result = repository.CommentComments(commId);
            return result;
        }
    }
}
