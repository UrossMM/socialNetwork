using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace socialNetwork.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Type { get; set; } // javna ili privatna
        //referenca na Group kojoj pripada Post
        public int GroupId { get; set; }
        [JsonIgnore]
        public Group Group { get; set; }
        public string UserId {get;set;}
        [JsonIgnore]
        public User User { get; set; }
        //referenca na User koji je napisao Post
        //referenca na komentare
        [JsonIgnore]
        public List<Comment> Comments { get; set; }
        
    }
}
