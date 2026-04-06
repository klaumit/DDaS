using System.Collections.Generic;
using DDaS.IO.API;
using DDaS.IO.Tools;

namespace DDaS.IO.Memory
{
    public sealed class MemDir : IDir
    {
        private readonly SortedDictionary<string, IEntry> _tracked;

        public MemDir(string name)
        {
            _tracked = [];
            Name = name;
        }

        public string Name { get; }

        public IFile GetFile(string name)
        {
            if (_tracked.TryGetValue(name, out var found))
                return (IFile)found;
            var file = new MemFile(name, this);
            _tracked.Add(file.Path, file);
            return file;
        }

        public void TrackFiles(params string[] patterns)
        {
            // NO-OP!
        }

        public override string ToString()
            => $"[M] {this.Path}";

        public void Dispose()
        {
            foreach (var entry in _tracked)
                entry.Value.Dispose();
            _tracked.Clear();
        }
    }
}