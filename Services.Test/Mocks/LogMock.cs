

using System;
using SimpelBlog.Logging;

namespace SimpelBlog.Mock
{
	public class LogMock : ISimpelLogger
	{
		public LogMock()
		{
		}

		public void LogDebug(string message)
		{
			Console.WriteLine(message);
		}

		public void LogError(string message, Exception e)
		{
			Console.WriteLine(message);
		}

		public void LogFatal(string message, Exception e)
		{
			Console.WriteLine(message);
		}

		public void LogInfo(string message)
		{
			Console.WriteLine(message);
		}

		public void LogStop()
		{

		}

		public void LogTrace(string message)
		{

			Console.WriteLine(message);
		}
	}
}
