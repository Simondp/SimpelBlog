using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SimpelBlog.Mock
{
    public class FileMock : IFormFile
    {
	private string _content;
	public FileMock(string content)
	{
		_content = content;
	}

        public string ContentType => throw new System.NotImplementedException();

        public string ContentDisposition => throw new System.NotImplementedException();

        public IHeaderDictionary Headers => throw new System.NotImplementedException();

        public long Length {get;set;}

        public string Name {get;set;}

        public string FileName {get;set;}

        public void CopyTo(Stream target)
        {
		target.Write(Encoding.Default.GetBytes(_content),0,_content.Length);
		target.Flush();
	}

        public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Stream OpenReadStream()
        {
            throw new System.NotImplementedException();
        }
    }

}
