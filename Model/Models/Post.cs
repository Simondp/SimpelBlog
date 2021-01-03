using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpelBlog.Model
{
    public class Post:BaseModel
    {
    	    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  
	    public Guid PostId{get;set;}
	    public DateTime createdOn{get;set;}
	    public DateTime updatedOn{get;set;}
	    public virtual PostInfo postInfo{get;set;}	
	    public string postAsHtml{get;set;}
	    public string postAsMarkdown{get;set;}
	    public List<Comment> comments {get;set;}
    }
}
