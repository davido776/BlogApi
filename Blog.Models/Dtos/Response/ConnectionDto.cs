using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Models.Dtos.Response
{
    public class ConnectionDto
    {
       
        
        public string Id { get; set; }
        public GetUserDto RequestBy { get; set; }
        public string connectionStatus { get; set; }

       
    }
}
