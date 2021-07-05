using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blog.Models
{
    public class Comment : BaseEntity
    {
        
        public string Body { get; set; }

        public string AppUserId { get; set; }

        public string PostId { get; set; }

        public AppUser AppUser { get; set; }
        public Post Post { get; set; }

        public int  NumberOfLikes { get; set; }

       
    }
}
