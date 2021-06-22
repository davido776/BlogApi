using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Models.Dtos.Request
{
    public class UpdatePostDto
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Body { get; set; }
    }
}
