using Blog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Blog.Data
{
    public class BlogDbContext : IdentityDbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {

        }
        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Dislike> Dislikes { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Comment> Comments{ get; set; }


       
    }


   
}
