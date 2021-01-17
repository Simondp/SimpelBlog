using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using SimpelBlog.Logging;
using SimpelBlog.Model;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace SimpelBlog.Services
{
	public class PostService : IPost
	{
		private readonly BlogContext _context; 
		private readonly IMarkdown _markdownService;
		private readonly ISimpelLogger _logger;
		public PostService(ISimpelLogger logger,IMarkdown markdown, BlogContext context){
			_logger  = logger;
			_markdownService = markdown;
			_context = context;
		}

		public Post CreatePost(string post)
		{
			try
			{ 


				var postToCreate = GeneratePostWithPostInfo(post);
				_context.Add(postToCreate);
				postToCreate.createdOn = DateTime.Now;
				_context.SaveChanges();
				return postToCreate;

			}
			catch(Exception e)
			{
				StringBuilder sb = new StringBuilder();
				sb.AppendLine("An unexpected error ocurred calling PostService.CreatePost with :");
				sb.AppendLine($"post : {post}");
				_logger.LogError(sb.ToString(),e);
				throw e;
			}			 		    

		}


		public void DeletePost(Guid Id)
		{
			try
			{	

				var toRemove = new Post()
				{
					PostId = Id
				};
				_context.Posts.Remove(toRemove);


			}
			catch (Exception e)
			{
				_logger.LogError($"Something unexpected happened trying to delete post with GUID : \n GUID : {Id}",e); 
			}
		}

		public List<Post> GetAllPosts()
		{
			try
			{
				return _context.Posts.ToList();
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message,  e);
				throw e;
			}
		}

		public Post GetPost(Guid Id)
		{

			return _context.Posts
				.Single(P => P.PostId == Id );

		}

		public void UpdatePost(Guid Id, string post)
		{
			try
			{
				var postToupdate = GeneratePostWithPostInfo(post);
				postToupdate.PostId = Id;
				postToupdate.updatedOn = DateTime.Now;
				_context.Update(postToupdate);
				_context.SaveChanges();
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message, e);	
			}
		}
		public Post GeneratePostWithPostInfo(string postContent)
		{
			try{
				Post post = new Post();
				var deserializer = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();	
				var yaml = _markdownService.GetPostMetadataFromMarkDown(postContent);
				post.postAsMarkdown=postContent;
				postContent = postContent.Replace("---"+yaml+"---","");
				post.postInfo = deserializer.Deserialize<PostInfo>(yaml);
				post.postAsHtml=_markdownService.ParseMarkDownToHtml(postContent);	
				StringBuilder sb = new StringBuilder();
				sb.AppendLine("Generated post wih :");
				sb.AppendLine($"PostAsMarkDown : {post.Stringify(post)}");
				sb.AppendLine($"PostInfo :{yaml} {post.Stringify(post.postInfo)}");
				_logger.LogDebug(sb.ToString());
				return post;
			}
			catch(Exception e)
			{
				_logger.LogError($"Failed to generate post. {e.Message} ", e);
				throw e;
			}
		}


		public void AttachMediaToPost(Guid postId,List<IFormFile> files,string path)
		{
			var postTitel = (from p in _context.Posts 
					join i in _context.PostInfos on p.PostId equals i.PostId
					select i.titel).SingleOrDefault();
			if(postTitel == null)
			{
				throw new ArgumentException($" Post with Guid:{postId} either does not exists or have no titel");
			}
			else
			{
				Directory.CreateDirectory($"{path}/{postTitel}");
				UploadMediaToFolder(files,$"{path}/{postTitel}/");
			}
		}

		public void UploadMediaToFolder(List<IFormFile> files, string path)
		{
			try
			{
			foreach (var formFile in files)
			{
				if (formFile.Length > 0)
				{
					var filePath = path+formFile.FileName;

					using (var stream = File.Create(filePath))
					{
						formFile.CopyTo(stream);
						
					}
				}
			}
			}
			catch (Exception e)
			{
				StringBuilder sb = new StringBuilder();
				sb.AppendLine("Failed to save files :");
				foreach(var file in files)
				{
					sb.AppendLine($"File name : {file.FileName}");
				}
				sb.AppendLine($"To path : {path}");
				sb.AppendLine(e.Message);
				_logger.LogError(sb.ToString(), e);
			}

		}
	}
}
