using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Models.ViewModels
{
    public class StatusError
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public StatusError(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
