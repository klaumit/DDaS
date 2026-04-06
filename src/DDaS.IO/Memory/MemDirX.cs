using System.Collections.Generic;
using DDaS.IO.API;
using DDaS.IO.Tools;

namespace DDaS.IO.Memory
{
    public sealed class MemDirX : IDirX
    {
        private readonly SortedDictionary<string, IEntryX> _tracked;

        public MemDirX(string name)
        {
            _tracked = [];
            Name = name;
        }

        public string Name { get; }

        public IFileX GetFile(string name)
        {
            if (_tracked.TryGetValue(name, out var found))
                return (IFileX)found;
            var file = new MemFileX(name, this);
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