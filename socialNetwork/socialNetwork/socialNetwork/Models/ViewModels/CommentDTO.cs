using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Models.ViewModels
{
    public class CommentDTO
    {
        public string Text { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int? ParentId { get; set; } 
        public Comment? Parent { get; set; }
    }
}
