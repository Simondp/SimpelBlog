using SimpelBlog.Model;

namespace SimpelBlog.Services
{
	public interface IValidation
	{
		bool AuthenticateUser(string username,string password);
		string GenerateJWTToken(User user);
        string GenerateNewToken(string token);
    }

}
