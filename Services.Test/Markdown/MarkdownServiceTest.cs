using SimpelBlog.Services;
using NUnit.Framework;
using System;
using SimpelBlog.Mock;

namespace SimpelBlog.UnitTests.services
{
    [TestFixture]
	public class MarkdownServiceTest
	{
		IMarkdown _markdownService;
		[SetUp]
		public void Setup()
		{
			LogMock log= new LogMock(); 
			_markdownService = new MarkdownService(log);

		}
		
		[TestCase("--- Hello World ---")]
		public void ExtractMetadataFromPostTest(string markdown)
		{
			var result = _markdownService.GetPostMetadataFromMarkDown(markdown);
			Console.WriteLine(result);
			Assert.True(result.Equals(" Hello World "));
			
		}


	}

}
