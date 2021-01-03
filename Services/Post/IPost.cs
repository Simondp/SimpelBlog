using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using SimpelBlog.Model;

namespace SimpelBlog.Services
{
    public interface IPost
	{
		List<Post> GetAllPosts();
		Post GetPost(Guid Id);
		Post CreatePost(string post);
		void AttachMediaToPost(Guid postId,List<IFormFile> files,string path);
		void DeletePost(Guid Id);
		void UpdatePost(Guid Id,string post);
	}
}
