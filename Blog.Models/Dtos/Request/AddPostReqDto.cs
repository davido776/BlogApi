using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Models.Dtos.Request
{
    public class AddPostReqDto
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Body { get; set; }
       
        public IFormFile PostPhoto { get; set; }
        
    }
}
