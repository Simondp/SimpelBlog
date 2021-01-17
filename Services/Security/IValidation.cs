using SimpelBlog.Model;

namespace SimpelBlog.Services
{
	public interface IValidation
	{
		User AuthenticateUser(User user);
		string GenerateJWTToken(User user);
	}

}
