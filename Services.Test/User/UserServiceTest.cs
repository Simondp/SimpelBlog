using SimpelBlog.Model;
using SimpelBlog.Services;
using NUnit.Framework;
using System.Linq;
using SimpelBlog.Logging.Exceptions;
using SimpelBlog.Mock;
using System;

namespace SimpleBlog.UnitTests.Services
{
	public class UserServiceTest
	{
		private BlogContext _ctx;
		private UserService _userService;	
		[SetUp]
		public void SetUp()
		{

			LogMock log = new LogMock();
			MarkdownServiceMock ms = new MarkdownServiceMock();
			_ctx = MockContext.GetContext();
			_userService = new UserService(log,_ctx);

		}
		[Test]
		public void IsFirstUser_UserTableNotEmpty_returnfalse()
		{
			Assert.False(_userService.IsFirstUser());
		}
		[Test]
		public void IsFirstUser_UserTableEmpty_ReturnTrue()
		{
			Assert.True(_userService.IsFirstUser());
		}
		
		[TestCase(null,"test123")]
		public void IsValidUser_NoUserName_ReturnsFalse(string userName,string pass)
		{
			User user = new User
			{
				username = userName,
				password = pass
			};
			Assert.False(_userService.IsValidUser(user));
			
		}
		[TestCase("Test","")]
		public void IsValidUser_NoPassword_ReturnsFalse(string userName,string pass)
		{
			User user = new User
			{
				username = userName,
				password = pass
			};
			Assert.False(_userService.IsValidUser(user));
			
		}
		[TestCase("Test","test123")]
		public void IsValidUser_UsernameAlereadyExists_ReturnsFalse(string userName,string pass)
		{
			var existingUser = new User
			{
				username = userName,
				password = pass	 
			};
			_ctx.Add(existingUser);
			_ctx.SaveChanges();

			User user = new User
			{
				username = userName,
				password = pass
			};
			Assert.False(_userService.IsValidUser(user));
			
		}
		[TestCase("Test","test123")]
		public void IsValidUser_FirstUser_ReturnsTrue(string userName,string pass)
		{

			User user = new User
			{
				username = userName,
				password = pass
			};
			Assert.True(_userService.IsValidUser(user));
			
		}
		[TestCase("Test","test123")]
		public void CreateUser_FirstUser_ReturnsUser(string userName,string pass)
		{
			var user = new User
			{
				username = userName,
				password = pass	 
			};
			_userService.CreateUser(user);
			var storedUser = (from u in _ctx.Users
				   where u.username == userName
				   select u).First();
			Assert.True(storedUser.username.Equals(userName));
		}
		[TestCase("Test","test123")]
		public void GetUserById_UserExists_ReturnsUser(string userName,string pass)
		{
			var user = new User
			{
				username = userName,
				password = pass	 
			};
			_ctx.Add(user);
			_ctx.SaveChanges();

			var storedUser = _userService.GetUserByUsername(userName); 
			Assert.True(userName.Equals(storedUser.username));
		}
		[TestCase("Test","test123")]
		public void GetUserById_UserDoesNotExist_throwsException(string userName,string pass)
		{
			Assert.Throws<UsernameNotFoundException>(()=>_userService.GetUserByUsername(userName));
		}
		[TestCase("Test","test123","newTest")]
		public void UpdateUser_UserExists_UpdatesUser(string userName,string pass,string newUsername)
		{
		
			var user = new User
			{
				username = userName,
				password = pass	 
			};
			_ctx.Add(user);
			_ctx.SaveChanges();
			var storedUser = (from u in _ctx.Users
				   where u.username == userName
				   select u).First();
			var userToUpdate = new User
			{
				Id = storedUser.Id,
				username = newUsername   
					
			};
			_userService.UpdateUser(userToUpdate);	
			
			storedUser = (from u in _ctx.Users
				   where u.username == newUsername
				   select u).First();
			Assert.True(storedUser.username.Equals(newUsername));
		}
		[TestCase("Test","test123")]
		public void DeleteUserById_UserExists_DeletesUser(string userName,string pass)
		{
			var user = new User
			{
				username = userName,
				password = pass	 
			};
			_ctx.Add(user);
			_ctx.SaveChanges();

			var storedUser = (from u in _ctx.Users
					where u.username == userName
					select u).First();
			_userService.DeleteUserById(storedUser.Id);

			var result = (from u in _ctx.Users
					where u.username == userName
					select u).ToList();
			Assert.False(result.Any());
		}
		[TestCase("Test","test123")]
		public void DeleteUserById_UserDoesNotExist_throwsException(string userName,string pass)
		{
			Assert.Throws<UserNotFoundException>(()=>_userService.DeleteUserById(new Guid()));
		}
	}	
}
