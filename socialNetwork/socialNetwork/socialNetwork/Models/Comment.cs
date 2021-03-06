using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace socialNetwork.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        //referenca na objavu kojoj pripada Comment
        public int PostId {get;set;}
        [JsonIgnore]
        public Post Post { get; set; }
        // da li treba pamtiti root komentar?
        public int? ParentId { get; set; } //id na roditeljski komentar
        [JsonIgnore]
        public Comment? Parent { get; set; }
    }
}
