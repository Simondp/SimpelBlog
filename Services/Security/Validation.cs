using System;
using SimpelBlog.Logging;
using SimpelBlog.Model;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace SimpelBlog.Services
{
    public class Validation : IValidation
	{
		private readonly IConfiguration _config;
		private readonly ISimpelLogger _logger;
		private readonly BlogContext _context;
		public Validation(ISimpelLogger logger,IConfiguration config ,BlogContext context)
		{
			_config = config;
			_context = context;
			_logger = logger;
		}
		public User AuthenticateUser(User user)
		{
			try
			{
				return (from u in _context.Users 
						where u.username == user.username && u.password == user.password
						select u).SingleOrDefault();
			}
			catch(Exception e)
			{
				_logger.LogError("Failed to validate user",  e);
				return null;
			}
		}

		public string GenerateJWTToken(User user)
		{
			try
			{
				var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
				var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
				var token = new JwtSecurityToken(
						issuer: _config["Jwt:Issuer"],
						audience: _config["Jwt:Audience"],
						expires: DateTime.Now.AddMinutes(30),
						signingCredentials: credentials
						);
				return new JwtSecurityTokenHandler().WriteToken(token);
			}	
			catch(Exception e)
			{
				_logger.LogError("Error generation JWT",  e);
				return null;
			}
		}
	}

}
