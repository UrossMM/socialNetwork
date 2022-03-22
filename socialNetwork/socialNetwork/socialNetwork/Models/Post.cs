using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Type { get; set; } // javna ili privatna
        //referenca na Group kojoj pripada Post
        //referenca na User koji je napisao Post
        //referenca na komentare
        
    }
}
