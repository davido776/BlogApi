using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Models.Dtos.Response
{
    public class AddPostResDto
    {
        public string PostId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
