using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GW.Site.Tests.Fakes
{
    class FakeFormFile : IFormFile
    {
        public string ContentType { get; set; }

        public string ContentDisposition => throw new NotImplementedException();

        public IHeaderDictionary Headers => throw new NotImplementedException();

        public long Length { get; set; }

        public string Name { get; set; }

        public string FileName { get; set; }

        public void CopyTo(Stream target)
        {
            throw new NotImplementedException();
        }

        public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Stream OpenReadStream()
        {
            return new MemoryStream(new byte[Length]);
        }
    }
}
