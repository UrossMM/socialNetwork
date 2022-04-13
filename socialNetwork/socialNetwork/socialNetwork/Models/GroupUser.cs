using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace socialNetwork.Models
{
    public class GroupUser
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public int GroupId { get; set; }
        [JsonIgnore]
        public Group Group { get; set; }
        // da li treba reference na usera i grupu? -za sad stavljeno
    }
}
