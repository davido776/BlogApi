using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Models.Dtos.Response
{
    public class GetPostDto
    {
       
        
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Body { get; set; }
       
        public string ImageUrl { get; set; }
        public int NumberOfLikes { get; set; } 

        public ICollection<GetLikeDto> Likes { get; set; }
       
    }
}
