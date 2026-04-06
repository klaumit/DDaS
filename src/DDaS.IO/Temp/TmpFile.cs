using System.IO;
using DDaS.IO.API;
using DDaS.IO.Tools;

namespace DDaS.IO.Temp
{
    public sealed class TmpFile : IFile
    {
        private FileStream? _stream;
        private readonly string _real;

        public TmpFile(string name, IDir dir, string real)
        {
            _real = real;
            Dir = dir;
            Name = name;
            Mime = this.GetMimeFromExt();
        }

        public IDir Dir { get; }
        public string Name { get; }
        public string Mime { get; set; }

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