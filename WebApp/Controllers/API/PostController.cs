using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpelBlog.Logging;
using SimpelBlog.Model;
using SimpelBlog.Services;

namespace WebApp.Controllers.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class PostController:ControllerBase
	{
		private readonly IPost _postService;
		private readonly ISimpelLogger _logger;
		public PostController(ISimpelLogger logger, IPost postService){
			_postService = postService;
			_logger = logger;
		}


		[HttpGet("posts")]
		public async Task <ActionResult<List<Post>>> GetAllPosts(){

			try
			{
				_logger.LogDebug($"ApiControllers GetAllPostsMetadata");
				return  _postService.GetAllPosts();
			}
			catch(Exception e)
			{
				StringBuilder sb = new StringBuilder("An unexpected error happend calling _postService.GetAllPostsMetadata");
				_logger.LogError(sb.ToString(),e);
				return BadRequest();
			}	

		}
		[HttpGet("post/{id}")]
		public async Task<ActionResult<Post>> GetPost(Guid id){

			try
			{
				_logger.LogDebug($"ApiControllers GetPost called with Id : {id}");
				return  _postService.GetPost(id);
			}
			catch(Exception e)
			{
				StringBuilder sb = new StringBuilder("An unexpected error happend calling _postService.GetPost() with :");
				sb.AppendLine($"id:{id}");
				_logger.LogError(sb.ToString(),e);
				return BadRequest();
			}
		}
		[HttpPost("post")]
		public async Task<ActionResult<Post>> CreatePost([FromBody]Post rawPost){
			
			
			try
			{
				_logger.LogDebug($"Postlength : {rawPost.postAsMarkdown.Length}");
				var post = _postService.CreatePost(rawPost.postAsMarkdown);
				return  CreatedAtAction($"/blog/{post.postInfo.titel}",new{id = post.PostId });
			}
			catch(Exception e)
			{
				StringBuilder sb = new StringBuilder("An unexpected error happend calling _postService.CreatePost() with :");
				sb.AppendLine($"Post:{rawPost}");
				_logger.LogError(sb.ToString(),e);
				return BadRequest();
			}
		  
		} 

		  [HttpPost("media")]
		  public async Task<ActionResult> AttachMediaToPost(Guid id,List<IFormFile> files,IWebHostEnvironment _environment)
		  {
			try
			{
			
				_postService.AttachMediaToPost(id,files,_environment.WebRootPath);
				return Ok();
			}
			catch(Exception e)
			{
				_logger.LogError($"Failed to attahc media to post with GUID : {id} {e.Message}",e);
				return BadRequest("Failed to attach media to post, please check the log");
			}

		  }


	}

}
