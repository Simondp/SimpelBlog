using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpelBlog.Model
{
    public enum PostStatus
    {
	DRAFT,
	READY,
	PUBLISHED
    };
    public class PostInfo : BaseModel
    {

    	    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  
	    public Guid PostInfoId{get;set;}
	    public Guid PostId{get;set;}
	    public string recap {get;set;}
	    public string titel{get;set;}
	    public PostStatus postStatus{get;set;}
	    public DateTime publishDate{get;set;}
    	    public virtual Post post{get;set;} 	 	
    }

}
