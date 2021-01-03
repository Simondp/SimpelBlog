using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SimpelBlog.Logging;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
	    var logger = new SimpelLogger(); 
	    try
	    {
		    
		Console.Write("Hello world \n");    
		logger.LogDebug("Init main");    
            	CreateHostBuilder(args).Build().Run();
	    }
	    catch(Exception exception){
		logger.LogError("An unexpected error occured",exception);
        	throw;
	    }
	    finally
		{
			logger.LogStop();	
		}
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
      }
    }

