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
        [Route("addUserToGroup/{id}")]
        public IActionResult AddUserToGroup(string id, Group group)
        {
            repository.AddUserToGroup(id, group);
            return Ok();
        }
    }
}
