using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpelBlog.Model
{
    public class Comment : BaseModel
    {
    	    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	    public Guid Id{get;set;}
	    public DateTime createdOn{get;set;}
	    public DateTime updatedOn{get;set;}
	    public string comment{get;set;}
    }
}
