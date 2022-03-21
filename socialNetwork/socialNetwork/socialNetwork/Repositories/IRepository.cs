using socialNetwork.Models;
using socialNetwork.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Repositories
{
    public interface IRepository
    {
        void CreateGroup(Group group, UserVM user); // group je grupa koju admin kreira, ili umesto USERVM int idUsera??
        void Follow(UserVM user1, UserVM user2); //onaj ko pozove fju je onaj ko hoce da zaprati user1
        void CommentPost(Comment comm, Post post); // comm je komentar koji se dodaje na objavu post, ili umesto post samo idPost??
        void AddUserToGroup(UserVM user, Post post); // user se dodaje u post od strane admina(njegov id ne mora da se navodi?)
        void CreatePost(Post post, Group group, UserVM user); //kreira se post od strane user, ili saljes idGrupe i idUsera 


    }
}
