using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Blog.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            Addresses = new List<Address>();
            Post = new List<Post>();
            Connections = new List<Connection>();

        }

       

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }

        public ICollection<Address> Addresses { get; set; }
        public ICollection<Post> Post { get; set; }

       
        [ForeignKey("RequestedBy_Id")]
        [NotMapped]
        public virtual ICollection<Connection> Connections { get; set; }


        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; }
        public string ProfilePicUrl { get; set; }

    }
}
