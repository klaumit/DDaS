using System.IO;
using DDaS.IO.API;
using DDaS.IO.Tools;

namespace DDaS.IO.Temp
{
    public sealed class TmpFileX : IFileX
    {
        private FileStream? _stream;
        private readonly string? _real;

        public TmpFileX(string name, IDirX dir, string? real = null)
        {
            _real = real;
            Dir = dir;
            Name = name;
        }

        public IDirX Dir { get; }
        public string Name { get; }

        public Stream NewStream()
        {
            _stream?.Dispose();
            var fullPath = _real!;
            return _stream = new FileStream(fullPath, FileMode.Create);
        }

        public override string ToString()
            => $"[tmp] {this.Path}";

        public void Dispose()
        {
            _stream?.Dispose();
            FileExt.DeleteFile(_real);
        }
    }
}