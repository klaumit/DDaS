using System;
using System.IO;
using DDaS.Core.Models;
using DDaS.Core.Tools;
using Fil = System.IO.File;

namespace DDaS.Core.Common
{
    public sealed class TempFile : IFileObj, IExFileObj
    {
        public TempFile(string file, TempDir? folder = null)
        {
            Folder = folder;
            File = file;
            Name = Path.GetFileName(file);
            Bytes = FileTool.TryReadAllBytes(file) ?? [];
            Mime = Defaults.Octet;
        }

        public ITempDir? Folder { get; }
        public string File { get; }
        public string Name { get; }
        public byte[] Bytes { get; }
        public string Mime { get; }

        public void Dispose()
        {
            if (Fil.Exists(Name))
                Fil.Delete(Name);
        }
    }

    public sealed class TempFile3 
    {
        private readonly string _path;

        public TempFile3(string path)
        {
            _path = path;
        }

        public void Dispose()
        {
        }

        public byte[] Bytes => throw new InvalidOperationException(_path);
        
        public string Name => throw new InvalidOperationException(_path);

        public string GetNewName(string ext) => throw new NotImplementedException(ext);
        
        public string GetNewName(string suf, object tmpDir) => throw new NotImplementedException();
    }
}