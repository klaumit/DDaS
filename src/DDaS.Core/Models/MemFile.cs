using DDaS.Core.Models;

namespace DDaS.Tests.Web.Tools
{
    public record MemFile(string Name, byte[] Bytes, string Mime) : IFileObj
    {
        public void Dispose()
        {
        }
    }
}