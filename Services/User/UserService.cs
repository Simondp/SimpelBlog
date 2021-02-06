using System;
using System.Collections.Generic;
using SimpelBlog.Logging;
using SimpelBlog.Model;
using SimpleBlog.Services;
using System.Linq;
using SimpelBlog.Logging.Exceptions;

namespace SimpelBlog.Services
{
	public class UserService : IUserService
	{ 
		private readonly ISimpelLogger _logger;
		private readonly BlogContext _ctx;
		public UserService(ISimpelLogger logger, BlogContext ctx){
			_logger = logger;
			_ctx = ctx;	
		}

		public void CreateUser(User user)
		{

			if(IsValidUser(user))
			{
				_ctx.Add(user);
				_ctx.SaveChanges();
			}
		}

		public bool IsValidUser(User user)
		{
			try
			{
				var validUser = !string.IsNullOrEmpty(user.username) && !string.IsNullOrEmpty(user.password);
				var userExists = (from u in _ctx.Users
						where u.username.Equals(user.username)
						select u).ToList().Any();
				return validUser && !userExists;
			}
			catch(Exception e)
			{
				throw e;
			}
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
				return (from u in _ctx.Users
						where u.username == username	
						select u).First();
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
				var storedUser = (from u in _ctx.Users
						where u.Id == user.Id
						select u).First();
				//Update relevant fields.
				storedUser.username = user.username ?? storedUser.username;
				storedUser.firstName = user.firstName ?? storedUser.firstName;
				storedUser.lastName = user.lastName ?? storedUser.lastName;
				storedUser.bio = user.bio ?? storedUser.bio;
				storedUser.password = user.password ?? storedUser.password;

				_ctx.Update(storedUser);
				_ctx.SaveChanges();
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
				var user = (from u in _ctx.Users
						where u.Id == id
						select u).First();
				_ctx.Remove(user);
				_ctx.SaveChanges();
			}
			catch(Exception e)
			{
				throw new UserNotFoundException($"Could not delete user with id {id}",e);
			}
		}

	}
}
