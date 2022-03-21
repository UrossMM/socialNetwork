using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //referenca na admina grupe (User)
        //referenca na tabelu PripadaGrupi
        //referenca na tabelu Post
    }
}
