using System.IO;
using DDaS.IO.API;
using DDaS.IO.Tools;

namespace DDaS.IO.Temp
{
    public sealed class TmpFileX : IFile
    {
        private FileStream? _stream;
        private readonly string _real;

        public TmpFileX(string name, IDir dir, string real)
        {
            _real = real;
            Dir = dir;
            Name = name;
        }

        public IDir Dir { get; }
        public string Name { get; }
        
        public string Mime => this.GetMimeFromExt();

        public Stream NewStream()
        {
            _stream.FlushDispose();
            return _stream = new FileStream(_real, FileMode.Create);
        }

        public byte[] Bytes
        {
            get
            {
                _stream.FlushDispose();
                return FileExt.ReadBytes(_real);
            }
        }

        public override string ToString()
            => $"[T] {this.Path}";

        public void Dispose()
        {
            _stream.FlushDispose();
            FileExt.DeleteFile(_real);
        }
    }
}