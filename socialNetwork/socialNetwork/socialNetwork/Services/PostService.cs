using socialNetwork.Models;
using socialNetwork.Models.ViewModels;
using socialNetwork.Paging;
using socialNetwork.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepo _postRepo;

        public PostService(IPostRepo postRepo)
        {
            _postRepo = postRepo;
        }

        public Comment AddComment(Comment newComm, int postId, int? parentComm = null)
        {
            return _postRepo.AddComment(newComm, postId, parentComm);
        }

        public List<CommentDTO> CommentComments(int commId)
        {
            return _postRepo.CommentComments(commId);
        }

        public Post CreatePost(Post post, int groupId, string userId)
        {
            return _postRepo.CreatePost(post, groupId, userId);
        }

        public List<PostDTO> GetPosts(int groupId, string user, PagingProperties prop)
        {
            return _postRepo.GetPosts(groupId, user, prop);
        }

        public List<CommentDTO> PostComments(int postId)
        {
            return _postRepo.PostComments(postId);
        }
    }
}
