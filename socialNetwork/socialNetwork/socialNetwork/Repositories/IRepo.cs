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
        void CreateGroup(Group group, string user); // group je grupa koju admin kreira, ili umesto USERVM int idUsera??
        void Follow(UserDTO user1, UserDTO user2); //onaj ko pozove fju je onaj ko hoce da zaprati user1
        void CommentPost(Comment comm, Post post); // comm je komentar koji se dodaje na objavu post, ili umesto post samo idPost??
        void AddUserToGroup(string id, Group group); // user se dodaje u post od strane ADMINA(njegov id ne mora da se navodi?)
        void CreatePost(Post post, Group group, UserDTO user); //kreira se post od strane user u grupi group, ili saljes idGrupe i idUsera 
        void GetAllPosts(Group group); //pribavljanje objava(preko paginacije), admin vidi sve objave u grupi dok ostali vide u zavisnosti od tipa 
        List<Group> GetAllGroups();
        Group GetGroupById(int id);
        List<Group> MyGroups(string myId); // vraca sve grupe koje je kreirao admin
    }
}
