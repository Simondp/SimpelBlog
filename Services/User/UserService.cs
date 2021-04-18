using System;
using System.Collections.Generic;
using SimpelBlog.Logging;
using SimpelBlog.Model;
using SimpleBlog.Services;
using System.Linq;
using SimpelBlog.Logging.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace SimpelBlog.Services
{
	public class UserService : IUserService 
	{ 
		private readonly UserManager<User> _userManager;
		private readonly ISimpelLogger _logger;
		private readonly BlogContext _ctx;
		public UserService(ISimpelLogger logger, BlogContext ctx,UserManager<User> userManager){
			_logger = logger;
			_ctx = ctx;	
			_userManager = userManager;
		}

		public void CreateUser(User user,string password)
		{
            
			var newUser = _userManager.CreateAsync(user,password).Result;
		}
        public void AddRolesToUser(User user, List<string> roles)
        {
            _userManager.AddToRolesAsync(user,roles).Wait();
        }

        public List<string> GetUserRoles(User user)
        {
            
                return _userManager.GetRolesAsync(user).Result as List<string>;
               
        }

		public bool IsFirstUser()
		{
			var users = (from u in _ctx.Users 
					select u).ToList();
			return !users.Any();
		}

		public List<User> GetAllUsers()
		{
			try
			{
				return (from u in _ctx.Users
						select u).ToList();
			}
			catch (Exception e)
			{
				throw new Exception("Unexpected error happend fetching all users",e);	
			}
		}

		public User GetUserByUsername(string username)
		{
			try
			{
				return _userManager.FindByNameAsync(username).Result;
			}
			catch(Exception e)
			{
				throw new UsernameNotFoundException($"Unexpected error happend getting user with username {username}",e);
			}
		}

		public void UpdateUser(User user)
		{
			try
			{
				_userManager.UpdateAsync(user);
			}
			catch(Exception e)
			{
				throw new Exception($"Unexpected error happend updating user with Guid {user.Id}",e);
			}
		}

		public void DeleteUserById(Guid id)
		{
			try
			{
				
			
				var userToDelete = _userManager.FindByIdAsync(id.ToString()).Result;
				_userManager.DeleteAsync(userToDelete);
			}
			catch(Exception e)
			{
				throw new UserNotFoundException($"Could not delete user with id {id}",e);
			}
		}

        public bool ValidatePassword(string username, string password)
        {
            var user = GetUserByUsername(username);
            return _userManager.CheckPasswordAsync(user,password).Result;
        }
    }
}
