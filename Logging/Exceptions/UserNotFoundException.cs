
using System;

namespace SimpelBlog.Logging.Exceptions
{
	[Serializable]
	public class UserNotFoundException : Exception
	{
		public UserNotFoundException(string message,Exception e) : base()
		{
		}
	}
}
