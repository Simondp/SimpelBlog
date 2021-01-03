using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpelBlog.Model
{
    public class User : BaseModel
    {
    	    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	    public Guid Id{get;set;}
	    public DateTime createdOn{get;set;}
	    public DateTime updatedOn{get;set;}
	    public string firstName{get;set;}
	    public string lastName{get;set;}
	    public string bio{get;set;}
	    public string username{get;set;}
	    public string password{get;set;}
    }
}
