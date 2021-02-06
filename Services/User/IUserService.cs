using System;
using System.Collections.Generic;
using SimpelBlog.Model;

namespace SimpleBlog.Services
{
	public interface IUserService
	{
		void CreateUser(User user);
		List<User> GetAllUsers();
		User GetUserByUsername(string username);
		void UpdateUser(User user);
		void DeleteUserById(Guid id);
	}
}
