using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DDaS.IO.API;
using DDaS.IO.Tools;
using Microsoft.Extensions.FileSystemGlobbing;

namespace DDaS.IO.Temp
{
    public sealed class TmpDirX : IDirX
    {
        private readonly SortedDictionary<string, IEntryX> _tracked;
        private readonly string? _real;

        public TmpDirX(string name, string? real)
        {
            _tracked = [];
            _real = FileExt.CreateTempDir(real);
            Name = name;
        }

        public string Name { get; }

        public IFileX GetFile(string name)
        {
            return GetTrackFile(name, forceAdd: true);
        }

        private IFileX GetTrackFile(string fileName, bool forceAdd = false)
        {
            if (!string.IsNullOrWhiteSpace(_real))
                fileName = Path.Combine(_real, fileName);
            if (_tracked.TryGetValue(fileName, out var found))
                return (IFileX)found;
            var label = Path.GetFileName(fileName);
            var file = new TmpFileX(label, this, fileName);
            if (forceAdd || File.Exists(fileName))
                _tracked.Add(fileName, file);
            return file;
        }

        public void TrackFiles(params string[] patterns)
        {
            if (string.IsNullOrWhiteSpace(_real))
                return;
            var matcher = new Matcher();
            matcher.AddIncludePatterns(patterns);
            foreach (var found in matcher.GetResultsInFullPath(_real))
                _ = GetFile(found);
        }

        private void CleanUp()
        {
            foreach (var entry in _tracked)
                entry.Value.Dispose();
            var files = FileExt.GetAllFiles(_real);
            if (files.Count == 0)
            {
                FileExt.DeleteDir(_real);
                return;
            }
            var nl = Environment.NewLine;
            var list = string.Join(nl, files.Select(f => $" * {f.FullName}"));
            throw new InvalidOperationException($"There are still remaining files!{nl}{list}");
        }

        public override string ToString()
            => $"[tmp] {this.Path}";

        public void Dispose()
        {
            CleanUp();
            _tracked.Clear();
        }
    }
}