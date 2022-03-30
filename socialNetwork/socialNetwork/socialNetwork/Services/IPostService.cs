using socialNetwork.Models;
using socialNetwork.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Services
{
    public interface IPostService
    {
        Post CreatePost(Post post, int groupId, string userId); 
        Comment AddComment(Comment newComm, int postId, int? parentComm = null); 
        List<string> GetPosts(int groupId, string user, int? pageNumber); 
        List<CommentDTO> PostComments(int postId); 
        List<CommentDTO> CommentComments(int commId);
    }
}
