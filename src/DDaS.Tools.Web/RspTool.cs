using System;
using System.Text;
using DDaS.IO.API;
using DDaS.IO.Memory;
using DDaS.Core.Models;
using DDaS.IO.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static DDaS.Server.Tools.WebTool;

namespace DDaS.Tools.Web
{
    public static class RspTool
    {
        public static Executed GetExecuted(this HttpContext ctx, FileContentResult fc)
        {
            var resEx = -1;
            var resMs = -1;
            string? resOt = null;
            var headers = ctx.Response.Headers;
            if (headers.TryGetValue(DdasRet, out var ret) &&
                ret[0]?.Split(" ; ", 2) is { } retP &&
                retP.Length == 2)
            {
                resEx = int.Parse(retP[0]);
                resMs = int.Parse(retP[1]);
            }
            if (headers.TryGetValue(DdasOut, out var oot) &&
                oot[0] is { } retO)
            {
                resOt = Encoding.UTF8.GetString(Convert.FromBase64String(retO));
            }
            var file = new MemFileX(fc.FileDownloadName).WriteTo(fc.FileContents);
            file.Mime = fc.ContentType;
            return new Executed(file, resMs, resEx, resOt);
        }
    }
}