using System;
using Microsoft.AspNetCore.Identity;

namespace SimpelBlog.Model
{
    public class User : IdentityUser<Guid>
    {
	    public string firstName{get;set;}
	    public string lastName{get;set;}
	    public string bio{get;set;}
    }
}
