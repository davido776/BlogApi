using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Models
{
    public class Dislike
    {
        public string Id { get; set; }
        public string AppUserId { get; set; }
        public string EntityId { get; set; }
    }
}
