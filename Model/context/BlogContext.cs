using Microsoft.EntityFrameworkCore;

namespace SimpelBlog.Model

{
    public class BlogContext : DbContext
	{
		public DbSet<PostInfo> PostInfos{get;set;}
		public DbSet<Post> Posts{get; set;}
		public DbSet<User> Users{get;set;}
		public DbSet<Comment> Comments{get;set;}
		public BlogContext(DbContextOptions<BlogContext> options):base(options){}

	//	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	//	{
	//		optionsBuilder.UseMySQL(Cong)
	//	}
		
	}


}
