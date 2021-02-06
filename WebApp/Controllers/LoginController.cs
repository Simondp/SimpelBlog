


using Microsoft.AspNetCore.Mvc;
using SimpelBlog.Logging;
using SimpleBlog.Services;

namespace Webapp.Controller
{
	public class LoginController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly ISimpelLogger _logger;
		public LoginController(ISimpelLogger logger,IUserService userService)
		{
			_userService = userService;
			_logger=logger;
		}
	}
}
