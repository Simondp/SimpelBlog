using System;
namespace SimpelBlog.Logging
{
	public interface ISimpelLogger
	{
	 void LogFatal(string message,Exception e);
	 void LogError(string message,Exception e);
	 void LogTrace(string message);
	 void LogInfo(string message);
	 void LogDebug(string message);
	 void LogStop();			
	}
}
