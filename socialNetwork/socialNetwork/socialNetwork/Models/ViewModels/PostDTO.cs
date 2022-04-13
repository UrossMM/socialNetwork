using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Models.ViewModels
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserId { get; set; }
        //public string FollowerId { get; set; }
        //idposta, datetime, userid, username
    }
}
