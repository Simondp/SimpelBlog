using Microsoft.EntityFrameworkCore;
using SimpelBlog.Model;

namespace SimpelBlog.Mock
{
	public class MockContext
	{
		public MockContext()
		{
		}
		public static BlogContext GetContext()
		{

			var dboptions= new DbContextOptionsBuilder<BlogContext>()
				.UseInMemoryDatabase(databaseName: "BlogDatabase")
				.Options;
			var dbContext = new BlogContext(dboptions);
			return dbContext;
		}
	}

}
