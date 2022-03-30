using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using socialNetwork.Attributes;
using socialNetwork.Models;
using socialNetwork.Models.ViewModels;
using socialNetwork.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Controllers
{
    [ApiKey]
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;

        public GroupController(IGroupService groupService, IMapper mapper)
        {
            _groupService = groupService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("createGroup")]
        public async Task<IActionResult> CreateGroup(Group group, string user)
        {
            var result = _groupService.CreateGroup(group, user);
            return Ok(result);
        }
        [HttpGet]
        [Route("getAllGroups")]
        public async Task<IActionResult> GetAllGroups()
        {
            return Ok(_groupService.GetAllGroups());

        }

        [HttpPost]
        [Route("getGroupById/{id}")]
        public async Task<IActionResult> GetGroupById(int id)
        {
            return Ok(_groupService.GetGroupById(id));

        }

        [HttpPost]
        [Route("getAdminGroups/{id}")]
        public IActionResult MyGroups(string id)
        {
            var result = _groupService.MyGroups(id);
            List<GroupDTO> res = _mapper.Map<List<GroupDTO>>(result);
            return Ok(res);

        }

        [HttpGet]
        [Route("allMembersInGroup/{groupId}")]
        public List<UserDTO> GroupMembers(int groupId)
        {
            var result = _groupService.GroupMembers(groupId);
            return result;
        }

        [HttpPost]
        [Route("addUserToGroup/{id}/{groupId}")]
        public IActionResult AddUserToGroup(string id, int groupId)
        {
            _groupService.AddUserToGroup(id, groupId);
            return Ok();
        }

        [HttpGet]
        [Route("allMyGroups/{myId}")]
        public List<GroupDTO> AllMyGroups(string myId)
        {
            var result = _groupService.AllMyGroups(myId);
            return result;
        }
    }
}
