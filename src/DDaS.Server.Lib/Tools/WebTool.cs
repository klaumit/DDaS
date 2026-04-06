using System.IO;
using System.Threading.Tasks;
using DDaS.Core.Common;
using DDaS.Core.Models;
using DDaS.Core.Tools;
using DDaS.IO.API;
using DDaS.IO.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static DDaS.Core.Tools.Defaults;

namespace DDaS.Server.Tools
{
    public static class WebTool
    {
        public static async Task<IFile> Save(IDir root, IFormFile file)
        {
            var name = Path.GetFileName(file.FileName);
            await using var stream = root.NewStream(name);
            await file.CopyToAsync(stream);
            await stream.FlushAsync();
            return root.GetFile(name);
        }

        public static IFormFile? IsEmpty(this IFormFile? file)
        {
            return file == null || file.Length == 0 ? null : file;
        }

        public static FileContentResult ToFile(ControllerBase ctrl, IFile file, string type = Octet)
        {
            var name = file.Name;
            var bytes = file.Bytes;
            return ctrl.File(bytes, type, name);
        }

        public const string DdasRet = "X-DDaS-Ret";
        public const string DdasOut = "X-DDaS-Out";

        public static void SetHeaders(this HttpContext ctx, Executed res)
        {
            var headers = ctx.Response.Headers;
            headers.Append(DdasRet, $"{res.Exit} ; {res.Ms}");
            if (res.Out.GetBase64() is { } bO)
                headers.Append(DdasOut, bO);
        }
    }
}