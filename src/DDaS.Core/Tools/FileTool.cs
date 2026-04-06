using System;
using System.IO;
using DDaS.Core.Common;
using DDaS.Core.Models;
using DDaS.IO.API;
using DDaS.IO.Temp;
using Fil = System.IO.File;

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

        public static IDirX GetDirectoryOf3(this IFileX input, out string root)
        {
            var parent = (input.Dir as TmpDirX)?.Real;
            var file = (input as IFileX)?.Name ?? input.Name;
            root = Path.GetFullPath(Path.GetDirectoryName(file) ?? "");
            return (IDirX)(object)parent!;
        }

        public static string GetNewName3(this IFileX input, string suf, string root = "")
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