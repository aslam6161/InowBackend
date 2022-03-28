using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InowBackend.Model
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
       
        public object Data { get; set; }

        public Response(bool status, string message, object data)
        {
            IsSuccess = status;
            Message = message;
            Data = data;
        }
    }
}
