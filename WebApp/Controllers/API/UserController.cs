using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SimpelBlog.Logging;
using SimpelBlog.Model;
using SimpelBlog.WebApp.DTO;
using SimpleBlog.Services;

namespace WebApp.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ISimpelLogger _logger;
        private readonly IUserService _userService;
        public UserController(IUserService userService, ISimpelLogger logger)
        {
            _logger = logger;
            _userService = userService;

        }
        [HttpPost]
        public async Task<ActionResult> CreateUser(UserDTO user)
        {
            try
            {
                Console.WriteLine(JsonConvert.SerializeObject(user));
                if(user.username == null || user.password == null)
                {
                    _logger.LogInfo($"Username or password was null \n user: {user.username} \n password: {user.password}");
                    return StatusCode((int)HttpStatusCode.BadRequest,"Username and password can't be null");
                }
                var UserToCreate = new User
                {
                    UserName = user.username   
                };
                _userService.CreateUser(UserToCreate, user.password);
                var createdUser = _userService.GetUserByUsername(user.username); 
                Console.WriteLine(JsonConvert.SerializeObject(createdUser));
                _userService.AddRolesToUser(createdUser,user.roles);
                Console.WriteLine(JsonConvert.SerializeObject(_userService.GetUserRoles(createdUser)));
                return StatusCode( (int) HttpStatusCode.Created);	

            }
            catch (Exception e)
            {

                _logger.LogError(e.Message,e);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }

    }

}
