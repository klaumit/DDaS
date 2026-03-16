using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Fil = System.IO.File;

namespace DDaS.Core
{
    internal static class FileTool
    {
        public static string WriteNewFile(string tmpDir, byte[] bytes, char p = 'a')
        {
            var ticks = DateTime.Now.Ticks;
            var file = Path.Combine(tmpDir, $"{p}{ticks}.bin");
            Fil.WriteAllBytes(file, bytes);
            return file;
        }

        public static IEnumerable<TempFile> Wrap(this IEnumerable<byte[]> arrays, string dir, char p = 'a')
        {
            return arrays.Select(a => new TempFile(dir, a, p));
        }

        public static string? CreateOrGetDir(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;
            var path = Path.GetFullPath(name);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }
    }
}