using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Models
{
    public class Like
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string AppUserId { get; set; }
        public string AppUserName { get; set; }
        public string PostId { get; set; }
        public Post Post { get; set; }
    }
}
