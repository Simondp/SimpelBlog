
using SimpelBlog.Services;

namespace SimpelBlog.Mock
{
    public class MarkdownServiceMock : IMarkdown
    {
        public string GetPostMetadataFromMarkDown(string markdown)
        {
            throw new System.NotImplementedException();
        }

        public string ParseMarkDownToHtml(string markdown)
        {
            throw new System.NotImplementedException();
        }
    }

}
