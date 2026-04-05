using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.FileSystemGlobbing;

namespace XyCon
{
    public sealed class TempDir : IDisposable
    {
        private readonly SortedSet<string> _tracked;

        public TempDir(string root)
        {
            _tracked = [];
            var myRoot = Path.GetFullPath(root);
            var myName = Path.GetRandomFileName().Replace(".", "")[..8];
            Folder = Path.Combine(myRoot, myName);
            Directory.CreateDirectory(Folder);
        }

        public string Folder { get; }

        public override string ToString() => Folder;

        private string GetFilePath(string fileName, bool forceAdd = false)
        {
            var fullPath = Path.Combine(Folder, fileName);
            if (forceAdd || File.Exists(fullPath)) _tracked.Add(fullPath);
            return fullPath;
        }

        public void WriteText(string fileName, string content)
        {
            var fullPath = GetFilePath(fileName, forceAdd: true);
            File.WriteAllText(fullPath, content, Encoding.UTF8);
        }

        public void WriteBytes(string fileName, byte[] content)
        {
            var fullPath = GetFilePath(fileName, forceAdd: true);
            File.WriteAllBytes(fullPath, content);
        }

        public string? ReadText(string fileName)
        {
            var fullPath = GetFilePath(fileName);
            return File.Exists(fullPath) ? File.ReadAllText(fullPath, Encoding.UTF8) : null;
        }

        public byte[]? ReadBytes(string fileName)
        {
            var fullPath = GetFilePath(fileName);
            return File.Exists(fullPath) ? File.ReadAllBytes(fullPath) : null;
        }

        private List<FileInfo> GetAllFiles()
            => Directory.EnumerateFiles(Folder, "*", SearchOption.AllDirectories)
                .Select(f => new FileInfo(f))
                .ToList();

        private void CleanUp()
        {
            foreach (var path in _tracked.Where(File.Exists))
                File.Delete(path);
            var files = GetAllFiles();
            if (files.Count == 0)
            {
                if (Directory.Exists(Folder))
                    Directory.Delete(Folder, recursive: true);
                return;
            }
            var nl = Environment.NewLine;
            var list = string.Join(nl, files.Select(f => $" * {f.FullName}"));
            throw new InvalidOperationException($"There are still remaining files!{nl}{list}");
        }

        public void Dispose()
        {
            CleanUp();
            _tracked.Clear();
        }

        public void TrackFiles(params string[] patterns)
        {
            var matcher = new Matcher();
            matcher.AddIncludePatterns(patterns);
            foreach (var matching in matcher.GetResultsInFullPath(Folder))
                _tracked.Add(matching);
        }
    }
}