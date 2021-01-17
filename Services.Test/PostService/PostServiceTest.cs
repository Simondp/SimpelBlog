using System.IO;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SimpelBlog.Mock;
using SimpelBlog.Model;
using SimpelBlog.Services;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
namespace SimpelBlog.UnitTests.Services
{
	[TestFixture]
	public class PostServiceTest
	{
		private BlogContext _ctx;
		private PostService _ps;	
		[SetUp]
		public void SetUp()
		{
			var dboption = new DbContextOptionsBuilder<BlogContext>()
				.UseInMemoryDatabase(databaseName: "BlogDatabase")
				.Options;
			LogMock log = new LogMock();
			MarkdownServiceMock ms = new MarkdownServiceMock();
			_ctx = new BlogContext(dboption);
			_ps = new PostService(log,ms,_ctx);
			_ctx.Posts.Add(new Post{postInfo = new PostInfo{titel="1 post"}});
			_ctx.SaveChanges();

		}

		[TestCase("1 post","Best post","Read this please")]
		public void UploadImageToFolderTest(string titel,string fileName,string content)
		{
			var guid = (from p in _ctx.Posts 
					join i in _ctx.PostInfos on p.PostId equals i.PostId
					where i.titel == titel 
					select p.PostId).SingleOrDefault();
			var file = new FileMock(content);
			file.Length = 10;
			file.FileName = fileName;
			List<IFormFile> files = new List<IFormFile>();
			files.Add(file);
			var path = Directory.GetCurrentDirectory();
			_ps.AttachMediaToPost( guid,files,path);			
			Assert.True(File.Exists($"{path}/{titel}/{fileName}"));
			Directory.Delete($"{path}/{titel}",true);

		}
	}
	
}
