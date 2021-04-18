using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SimpelBlog.Model

{
    public class BlogContext : IdentityDbContext<User,IdentityRole<Guid>,Guid>
	{
		public DbSet<PostInfo> PostInfos{get;set;}
		public DbSet<Post> Posts{get; set;}
		public DbSet<Comment> Comments{get;set;}
		public BlogContext(DbContextOptions<BlogContext> options):base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
        }
		
	}


}
