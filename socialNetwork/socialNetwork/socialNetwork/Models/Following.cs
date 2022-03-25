using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace socialNetwork.Models
{
    public class Following
    {
        public int Id { get; set; }
        public string FollowedId { get; set; }
        [JsonIgnore]
        public User Followed { get; set; }

        public string FollowerId { get; set; }
        [JsonIgnore]
        public User Follower { get; set; }
        // da li treba i reference na usere?? - za sada su dodate
    }
}
