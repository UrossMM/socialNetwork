using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Models
{
    public class ApiKeyUser
    {
        public int Id { get; set; }
        [StringLength(25)]
        public string Email { get; set; }
        [StringLength(40)]
        public string ApiKey { get; set; }

    }
}
