using System.IO;
using System.Threading.Tasks;
using DDaS.Core.Tools;
using Microsoft.AspNetCore.Http;

namespace DDaS.Server.Tools
{
    public static class WebTool
    {
        public const string Octet = "application/octet-stream";

        public static async Task<TempFile> Save(string root, IFormFile file)
        {
            var fullPath = Path.Combine(root, file.FileName);
            await using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);
            await stream.FlushAsync();
            return new TempFile(fullPath);
        }
    }
}