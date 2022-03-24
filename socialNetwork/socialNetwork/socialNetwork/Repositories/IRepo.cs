using socialNetwork.Models;
using socialNetwork.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Repositories
{
    public interface IRepo
    {
        Group CreateGroup(Group group, string user); // group je grupa koju admin kreira, ili umesto USERVM int idUsera??
        void Follow(string user1, string user2); //onaj ko pozove fju je onaj ko hoce da zaprati user1
        Comment AddComment(Comment newComm, int postId, int? parentComm = null); // comm je komentar koji se dodaje na objavu post, ili umesto post samo idPost??
        void AddUserToGroup(string id, int groupId ); // user se dodaje u post od strane ADMINA(njegov id ne mora da se navodi?)
        Post CreatePost(Post post, int groupId, string userId); //kreira se post od strane user u grupi group, ili saljes idGrupe i idUsera 
        List<string> GetPosts(int groupId, string user); //pribavljanje objava(preko paginacije), admin vidi sve objave u grupi dok ostali vide u zavisnosti od tipa 
        List<Group> GetAllGroups();
        Group GetGroupById(int id);
        List<Group> MyGroups(string myId); // vraca sve grupe koje je kreirao admin
    }
}
