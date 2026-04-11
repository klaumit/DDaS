using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDaS.IO.API;
using static DDaS.IO.API.Mimes;

// ReSharper disable FunctionRecursiveOnAllPaths
// ReSharper disable UnusedType.Global

namespace DDaS.IO.Tools
{
    public static class FileExt
    {
        extension(IEntry entry)
        {
            public string Path
            {
                get
                {
                    var parent = (entry as IFile)?.Dir?.Path;
                    var path = $"{parent}/{entry.Name}";
                    if (path.Length >= 2 && path.StartsWith('/'))
                        path = path[1..];
                    return path;
                }
            }
        }

        public static string GetMimeFromExt(this IFile file)
        {
            var name = Path.GetFileName(file.Name);
            var ext = Path.GetExtension(name).ToLowerInvariant();
            return ext switch
            {
                SymExt or AsmExt => AsmFile,
                ComExt => ComFile,
                CSrcExt => CSrcFile,
                CppExt => CppFile,
                PasExt => PasFile,
                _ => OctFile
            };
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

        public static async Task<T> WriteTo<T>(this T file, IEnumerable<string> lines) where T : IFile
        {
            await using var stream = file.NewStream();
            await using var writer = new StreamWriter(stream, Encoding.UTF8);
            foreach (var line in lines)
                await writer.WriteLineAsync(line.Trim());
            await writer.FlushAsync();
            return file;
        }

        public static T WriteTo<T>(this T file, string text) where T : IFile
        {
            return file.WriteTo(Encoding.UTF8.GetBytes(text));
        }

        public static T WriteTo<T>(this T file, byte[] bytes) where T : IFile
        {
            using var stream = file.NewStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
            return file;
        }

        public static T ReadTxt<T>(this T file, out string text) where T : IFile
        {
            var bytes = file.Bytes;
            text = Encoding.UTF8.GetString(bytes);
            return file;
        }

        public static IFile GetNewNamed(this IFile input, string ext)
        {
            var folder = input.GetDirectoryOf();
            var fileName = Path.GetFileNameWithoutExtension(input.Name);
            var newName = $"{fileName}.{ext.TrimStart('.')}";
            var newObj = folder.GetFile(newName);
            return newObj;
        }

        public static IDir GetDirectoryOf(this IFile input)
        {
            var folder = input.Dir;
            return folder!;
        }

        public static byte[] ReadBytes(this MemoryStream? stream)
        {
            return stream?.ToArray() ?? [];
        }

        public static byte[] ReadBytes(string filePath)
        {
            return File.Exists(filePath) ? File.ReadAllBytes(filePath) : [];
        }

        public static void FlushDispose(this Stream? stream)
        {
            if (stream == null)
                return;
            try
            {
                stream.Flush();
                stream.Dispose();
            }
            catch (ObjectDisposedException)
            {
                // Just ignore!
            }
        }

        public static Stream NewStream(this IDir root, string name)
        {
            var file = root.GetFile(name);
            return file.NewStream();
        }
    }
}