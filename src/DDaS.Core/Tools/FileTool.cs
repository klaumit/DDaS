using System;
using System.IO;
using DDaS.Core.Models;
using Fil = System.IO.File;

namespace DDaS.Core.Tools
{
    public static class FileTool
    {
        /*
        public static string WriteNewFile(string tmpDir, byte[] bytes, char p = 'a')
        {
            var ticks = DateTime.Now.Ticks;
            var file = Path.Combine(tmpDir, $"{p}{ticks}.bin");
            Fil.WriteAllBytes(file, bytes);
            return file;
        }
        */

        /*
        public static IEnumerable<TempFile> Wrap(this IEnumerable<byte[]> arrays, string dir, char p = 'a')
        {
            return arrays.Select(a => new TempFile(dir, a, p));
        }
        */

        public static string? CreateOrGetDir(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;
            var path = Path.GetFullPath(name);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }

        public static string GetDirectoryOf(this IFileObj input)
        {
            var file = (input as IExFileObj)?.File ?? input.Name;
            var dir = Path.GetDirectoryName(file);
            return Path.GetFullPath(dir ?? "");
        }

        public static string GetNewName(this IFileObj input, string suf, string root = "")
        {
            var baseName = Path.GetFileNameWithoutExtension(input.Name);
            var file = Path.Combine(root, $"{baseName}{suf}");
            return file;
        }

        public static byte[]? TryReadAllBytes(string file)
        {
            return Fil.Exists(file) ? Fil.ReadAllBytes(file) : null;
        }

        public static string GetEnvVarPath(string envName, string fallbackPath)
        {
            var text = Environment.GetEnvironmentVariable(envName);
            if (string.IsNullOrWhiteSpace(text)) text = fallbackPath;
            text = Environment.ExpandEnvironmentVariables(text);
            var path = Path.GetFullPath(text);
            return path;
        }
    }
}