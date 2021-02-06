using System;

namespace SimpelBlog.Logging.Exceptions
{
	[Serializable]
	public class UsernameNotFoundException : Exception
	{
		public UsernameNotFoundException(string message,Exception e) : base()
		{
		}
	}
}
