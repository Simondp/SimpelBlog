using NUnit.Framework;
using SimpelBlog.Mock;
using SimpelBlog.Model;
using SimpelBlog.Services;

namespace SimpelBlog.UnitTests.Services
{
	[TestFixture]
	public class PostServiceTest
	{
		[SetUp]
		public void SetUp()
		{
			LogMock log = new LogMock();
			MarkdownServiceMock ms = new MarkdownServiceMock(log);
			BlogContext ctx = new BlogContext();
			PostService _ps = new PostService(log,ms,ctx);	
		}

		[TestCase]
		public void UploadImageToFolderTest()
		{
		}
	}
	
}
