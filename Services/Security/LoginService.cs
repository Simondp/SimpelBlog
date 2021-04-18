using System;
using SimpelBlog.Logging;
using SimpelBlog.Model;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Collections.Generic;
using SimpleBlog.Services;
using System.Linq;

namespace SimpelBlog.Services
{
    public class LoginService : IValidation
	{
        private readonly IUserService _userService;
		private readonly IConfiguration _config;
		private readonly ISimpelLogger _logger;
		private readonly BlogContext _context;
		public LoginService(ISimpelLogger logger,IConfiguration config ,IUserService userService,SignInManager<User> signInManager,BlogContext context)
		{
            _userService = userService;
			_config = config;
			_context = context;
			_logger = logger;
		}
		public bool AuthenticateUser(string username,string password)
		{
			try
			{

				return _userService.ValidatePassword(username,password);
			}
			catch(Exception e)
			{
				_logger.LogError("Failed to validate user",  e);
				return false;
			}
		}

		public string GenerateJWTToken(User user)
		{
			try
			{
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)    
                };
                var roles = _userService.GetUserRoles(user);
                foreach(var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));   
                }
				var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
				var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
				var token = new JwtSecurityToken(
						issuer: _config["Jwt:Issuer"],
						audience: _config["Jwt:Audience"],
						expires: DateTime.Now.AddMinutes(30),
						signingCredentials: credentials,
                        claims : claims
						);
				return new JwtSecurityTokenHandler().WriteToken(token);
			}	
			catch(Exception e)
			{
				_logger.LogError("Error generation JWT",  e);
				return null;
			}
		}

        public string GenerateNewToken(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();

            var token = handler.ReadJwtToken(jwt) as JwtSecurityToken;

            var userName = token.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;

            var user = _userService.GetUserByUsername(userName);

            return GenerateJWTToken(user);
        }

	}

}
