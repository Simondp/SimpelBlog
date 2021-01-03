using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SimpelBlog.Model
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection RegisterDataService(this IServiceCollection services, IConfiguration configuration){
			Console.WriteLine($"DATA : {configuration.GetConnectionString("simpelBlogDev")}");
			services.AddDbContext<BlogContext>(o=>o.UseSqlServer(configuration.GetConnectionString("simpelBlogDev")));
			return services;
		}
		

	}


}
