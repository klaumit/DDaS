using System.IO;
using System.Threading.Tasks;
using DDaS.Core.API;
using DDaS.Core.Tools;
using DDaS.Server.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        public static IFormFile? IsEmpty(this IFormFile? file)
        {
            return file == null || file.Length == 0 ? null : file;
        }

        public static FileContentResult ToFile(ControllerBase ctrl, IFileObj file, string type = Octet)
        {
            var name = file.Name;
            var bytes = file.Bytes;
            return ctrl.File(bytes, type, name);
        }
    }
}