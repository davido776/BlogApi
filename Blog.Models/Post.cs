using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blog.Models
{
    public class Post : BaseEntity
    {
        public Post()
        {
            Comments = new List<Comment>();
            Likes = new List<Like>(); 
        }

        
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Body { get; set; }
        
        public string ImageUrl { get; set; }
        public int NumberOfLikes { get; set; }

        public ICollection<Like> Likes { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
