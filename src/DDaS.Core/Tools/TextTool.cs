namespace DDaS.Core.Tools
{
    public static class TextTool
    {
        public static string? GetNotNull(this string? text)
        {
            return string.IsNullOrWhiteSpace(text) ? null : text.Trim();
        }
    }
}