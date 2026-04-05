using System.IO;
using Microsoft.AspNetCore.Http;

// ReSharper disable NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract

namespace DDaS.Tests.Web.Tools
{
    public static class ReqTool
    {
        public static IFormFile AsFile(this byte[] bytes, string name, string? contentType = null)
        {
            var stream = new MemoryStream(bytes);
            const long offset = 0;
            var length = bytes.Length;
            var file = new FormFile(stream, offset, length, name, name);
            if (!string.IsNullOrWhiteSpace(contentType))
            {
                file.Headers ??= new HeaderDictionary();
                file.ContentType = contentType;
            }
            return file;
        }
    }
}