using System.Collections.Generic;

namespace SimpelBlog.WebApp.DTO
{
    public class UserDTO
    {
            public string username{get;set;}
            public string password{get;set;}
            public List<string> roles{get;set;}
    }

}           
