using System;
using System.Text.RegularExpressions;
using Markdig;
using SimpelBlog.Logging;

namespace SimpelBlog.Services
{
    public class MarkdownService : IMarkdown
	{
		private ISimpelLogger _logger;
		private  MarkdownPipeline _pipeline;
		public MarkdownService(ISimpelLogger logger)
		{
			_logger = logger;
			_pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().UseBootstrap().Build();
		}

		public string GetPostMetadataFromMarkDown(string markdown)
		{
			Regex reg = new Regex(@"---((.|\n)*)---");
			Match match = reg.Match(markdown);
			if(match == null )
			{
				throw new FormatException("Yaml not correct. Did you remember to have the yammel end and begin with '---'");
			}
			_logger.LogDebug($"Found match with value {match.Groups[1].Value}");

			return match.Groups[1].Value;
		}

		public string ParseMarkDownToHtml(string markdown)
		{
			try
			{
				return Markdig.Markdown.ToHtml(markdown, _pipeline);
			}
			catch (Exception e)
			{
				throw e;
			}
		}
	}
}
