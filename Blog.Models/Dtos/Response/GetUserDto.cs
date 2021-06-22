using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Models.Dtos.Response
{
    public class GetUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }

        public string ProfilePicUrl { get; set; }
    }
}
