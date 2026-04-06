using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DDaS.Core.Models;
using Microsoft.Extensions.FileSystemGlobbing;

namespace DDaS.Core.Common
{
    public sealed class TempDir : ITempDir
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

        private string Folder { get; }

        public override string ToString() => Folder;

        private string GetFilePath(string fileName, bool forceAdd = false)
        {
            var fullPath = Path.Combine(Folder, fileName);
            if (forceAdd || File.Exists(fullPath)) _tracked.Add(fullPath);
            return fullPath;
        }

        public Stream NewStream(string name)
        {
            var fullPath = GetFilePath(name, forceAdd: true);
            return new FileStream(fullPath, FileMode.Create);
        }

        public IFileObj GetFileRef(string name)
        {
            var fullPath = GetFilePath(name);
            return new TempFile(fullPath, folder: this);
        }

        public void TrackFile(string file)
        {
            GetFilePath(file, forceAdd: true);
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