using System.IO;
using DDaS.IO.API;
using DDaS.IO.Tools;

namespace DDaS.IO.Memory
{
    public sealed class MemFileX : IFileX
    {
        private MemoryStream? _stream;

        public MemFileX(string name, IDirX? dir = null)
        {
            Dir = dir;
            Name = name;
            Mime = this.GetMimeFromExt();
        }

        public IDirX? Dir { get; }
        public string Name { get; }
        public string Mime { get; set; }

        public Stream NewStream()
        {
            _stream.FlushDispose();
            return _stream = new MemoryStream();
        }

        public byte[] Bytes
            => _stream.ReadBytes();

        public override string ToString()
            => $"[M] {this.Path}";

        public void Dispose()
        {
            _stream.FlushDispose();
        }
    }
}