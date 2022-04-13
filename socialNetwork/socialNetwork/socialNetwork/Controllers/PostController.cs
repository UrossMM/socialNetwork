using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using socialNetwork.Attributes;
using socialNetwork.Models;
using socialNetwork.Models.ViewModels;
using socialNetwork.Paging;
using socialNetwork.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Controllers
{
    //[ApiKey]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        [Route("createPost/{groupId}/{userId}")]
        public IActionResult CreatePost(Post post, int groupId, string userId)
        {
            var result = _postService.CreatePost(post, groupId, userId);
            return Ok(result);
        }

        [HttpPost]
        [Route("addComment/{postId}")]
        public IActionResult AddComment(Comment newComm, int postId, [FromQuery] int? parentComm)
        {
            _postService.AddComment(newComm, postId, parentComm);
            return Ok();
        }

        //komentarisana jer je prebacena u SecretController
        /*[HttpPost] 
        [Route("getPosts/{groupId}/{user}")]
        public List<PostDTO> GetPosts(int groupId, string user, PagingProperties prop)
        {
            var result = _postService.GetPosts(groupId, user, prop);
            return result;
        }*/ 


        [HttpGet]
        [Route("postComments/{postId}")]
        public List<CommentDTO> PostComments(int postId)
        {
            var result = _postService.PostComments(postId);
            return result;
        }

        [HttpGet]
        [Route("commentComments/{commId}")]
        public List<CommentDTO> CommentComments(int commId)
        {
            var result = _postService.CommentComments(commId);
            return result;
        }
    }
}
