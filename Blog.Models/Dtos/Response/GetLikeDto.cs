using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Models.Dtos.Response
{
    public class GetLikeDto
    {
        public string AppUserId { get; set; }
        public string PostId { get; set; }

        public string AppUserName { get; set; }
    }
}
