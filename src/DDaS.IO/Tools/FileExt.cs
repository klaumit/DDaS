using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DDaS.IO.API;

// ReSharper disable FunctionRecursiveOnAllPaths
// ReSharper disable UnusedType.Global

namespace DDaS.IO.Tools
{
    public static class FileExt
    {
        extension(IEntryX entry)
        {
            public string Path
            {
                get
                {
                    var parent = (entry as IFileX)?.Dir.Path;
                    var path = $"{parent}/{entry.Name}";
                    if (path.Length >= 2 && path.StartsWith('/'))
                        path = path[1..];
                    return path;
                }
            }
        }

        extension(IFileX file)
        {
            public string Mime
            {
                get
                {
                    var name = Path.GetFileName(file.Name);
                    var ext = Path.GetExtension(name);
                    switch (ext)
                    {
                        default: throw new InvalidOperationException(name);
                    }
                }
            }
        }

        public static void DeleteFile(string? path)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
                return;
            File.Delete(path);
        }

        public static void DeleteDir(string? path)
        {
            if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
                return;
            Directory.Delete(path, recursive: true);
        }

        public static List<FileInfo> GetAllFiles(string? path)
        {
            if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
                return [];
            return Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories)
                .Select(f => new FileInfo(f))
                .ToList();
        }

        public static string? CreateTempDir(string? root)
        {
            if (string.IsNullOrWhiteSpace(root))
                return null;
            var myRoot = Path.GetFullPath(root);
            var myName = Path.GetRandomFileName().Replace(".", "")[..8];
            var folder = Path.Combine(myRoot, myName);
            Directory.CreateDirectory(folder);
            return folder;
        }

        public static void SaveTextIn(this Stream stream, IEnumerable<string> lines)
        {
            using var writer = new StreamWriter(stream, Encoding.UTF8);
            foreach (var line in lines)
                writer.WriteLine(line);
            writer.Flush();
        }
    }
}