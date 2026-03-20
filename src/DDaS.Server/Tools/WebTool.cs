using System.IO;
using System.Threading.Tasks;
using DDaS.Core.Models;
using DDaS.Core.Tools;
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

        public static void SetHeaders(this HttpContext ctx, Compiled res)
        {
            var headers = ctx.Response.Headers;
            headers.Append("X-DDaS-Dur", $"{res.Ms}");
            headers.Append("X-DDaS-Ret", $"{res.Exit}");
            if (res.Err != null)
                headers.Append("X-DDaS-Err", res.Err);
            if (res.Out != null)
                headers.Append("X-DDaS-Out", res.Out);
        }
    }
}