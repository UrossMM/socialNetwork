using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Models
{
    public class GroupUser
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        // da li treba reference na usera i grupu? -za sad stavljeno
    }
}
