

namespace SimpelBlog.Services
{
    public interface IMarkdown
	{
	 	string ParseMarkDownToHtml(string markdown);
		string GetPostMetadataFromMarkDown(string markdown);

	}

}
