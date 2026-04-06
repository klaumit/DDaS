using System.IO;
using DDaS.IO.API;
using DDaS.IO.Tools;

namespace DDaS.IO.Memory
{
    public sealed class MemFileX : IFileX
    {
        private MemoryStream? _stream;

        public MemFileX(string name, IDirX dir)
        {
            Dir = dir;
            Name = name;
        }

        public IDirX Dir { get; }
        public string Name { get; }

        public Stream NewStream()
        {
            _stream?.Dispose();
            return _stream = new MemoryStream();
        }

        public override string ToString()
            => $"[mem] {this.Path}";

        public void Dispose()
        {
            _stream?.Dispose();
        }
    }
}