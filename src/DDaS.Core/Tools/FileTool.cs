using System;
using System.IO;

namespace DDaS.Core.Tools
{
    public static class FileTool
    {
        public static string? CreateOrGetDir(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;
            var path = Path.GetFullPath(name);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }

        public static string GetEnvVar(string envName, string fallback)
        {
            var text = Environment.GetEnvironmentVariable(envName);
            if (string.IsNullOrWhiteSpace(text)) text = fallback;
            text = Environment.ExpandEnvironmentVariables(text);
            return text;
        }

        public static string GetEnvVarPath(string envName, string fallbackPath)
        {
            var text = GetEnvVar(envName, fallbackPath);
            var path = Path.GetFullPath(text);
            return path;
        }
    }
}