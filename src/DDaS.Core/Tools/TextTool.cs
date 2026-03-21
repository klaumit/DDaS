using System;
using System.Text;

namespace DDaS.Core.Tools
{
    public static class TextTool
    {
        public static string? GetNotNull(this string? text)
        {
            return string.IsNullOrWhiteSpace(text) ? null : text.Trim();
        }

        public static string? GetBase64(this string? txt)
        {
            return txt.GetNotNull() is { } t
                ? Convert.ToBase64String(Encoding.UTF8.GetBytes(t))
                : null;
        }
    }
}