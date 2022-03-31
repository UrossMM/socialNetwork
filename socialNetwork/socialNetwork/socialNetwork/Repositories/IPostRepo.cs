using socialNetwork.Models;
using socialNetwork.Models.ViewModels;
using socialNetwork.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Repositories
{
    public interface IPostRepo
    {
        Post CreatePost(Post post, int groupId, string userId); //kreira se post od strane user u grupi group, ili saljes idGrupe i idUsera 
        Comment AddComment(Comment newComm, int postId, int? parentComm = null); // comm je komentar koji se dodaje na objavu post, ili umesto post samo idPost??
        List<PostDTO> GetPosts(int groupId, string user, PagingProperties prop); //pribavljanje objava(preko paginacije), admin vidi sve objave u grupi dok ostali vide u zavisnosti od tipa 
        List<CommentDTO> PostComments(int postId); // komentari posta
        List<CommentDTO> CommentComments(int commId); //deca komentara
    }
}
