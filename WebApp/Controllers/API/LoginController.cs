


using System;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpelBlog.Logging;
using SimpelBlog.Services;
using SimpelBlog.WebApp.DTO;
using SimpleBlog.Services;

namespace Webapp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IValidation _validationService;
        private readonly IUserService _userService;
        private readonly ISimpelLogger _logger;
        public LoginController(ISimpelLogger logger,IUserService userService,IValidation validationService)
        {
            _userService = userService;
            _validationService = validationService;
            _logger=logger;
        }
        [HttpPost("signin")]
        public IActionResult login(UserDTO userDto)
        {
            try
            {
                if(_validationService.AuthenticateUser(userDto.username,userDto.password))
                {
                    var user = _userService.GetUserByUsername(userDto.username);
                    Response.Cookies.Append("Auth",_validationService.GenerateJWTToken(user), new CookieOptions
                            {
                                HttpOnly= true,
                                Secure = true,
                                SameSite = SameSiteMode.Strict

                            });
                    return StatusCode((int)HttpStatusCode.OK);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.Unauthorized);
                }
            }
            catch(Exception e)
            {
                _logger.LogError($"Unexpected error happend trying to log in user : \n Username:{userDto.username}",e);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }
        [HttpPost("renew")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult RenewLogin()
        {
            try
            {
                var token = Request.Cookies["Auth"];
                Response.Cookies.Append("Auth",_validationService.GenerateNewToken(token),new CookieOptions
                            {
                                HttpOnly= true,
                                Secure = true,
                                SameSite = SameSiteMode.Strict

                            });
                return StatusCode((int) HttpStatusCode.OK);
            }
            catch(Exception e)
            {
                _logger.LogError("Failed to renew token",e);
                return StatusCode((int)HttpStatusCode.InternalServerError,"Something went wrong, try to login and revesite the page");
            }
        }
        [HttpPost("signout")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Logout()
        {
            try
            {
            
                Response.Cookies.Delete("Auth");
                return StatusCode((int) HttpStatusCode.OK);
            }
            catch(Exception e)
            {
               _logger.LogError("Failed to log out user",e); 
                return StatusCode((int)HttpStatusCode.InternalServerError,"Something went wrong, try to login and revesite the page");
            }
        }

    }
}
