using System;
using System.Collections.Generic;
using SimpelBlog.Model;

namespace SimpleBlog.Services
{
	public interface IUserService
	{
        bool ValidatePassword(string username,string password);
		void CreateUser(User user,string password);
        void AddRolesToUser(User user, List<string> roles);
        List<string> GetUserRoles(User user);
		List<User> GetAllUsers();
		User GetUserByUsername(string username);
		void UpdateUser(User user);
		void DeleteUserById(Guid id);
	}
}
