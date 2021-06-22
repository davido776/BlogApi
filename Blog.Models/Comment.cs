using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blog.Models
{
    public class Comment
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Body { get; set; }

        public string AppUserId { get; set; }

        public string PostId { get; set; }

        public AppUser AppUser { get; set; }
        public Post Post { get; set; }

        public int  NumberOfLikes { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
