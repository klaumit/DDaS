using System;
using Fil = System.IO.File;

namespace DDaS.Core
{
    public sealed class TempFile : IDisposable
    {
        public string File { get; }
        public byte[] Bytes { get; }
        public int Size => Bytes.Length;

        public TempFile(string dir, byte[] bytes, char prefix = 'a')
        {
            File = FileTool.WriteNewFile(dir, Bytes = bytes, prefix);
        }

        public void Dispose()
        {
            if (!Fil.Exists(File))
                return;
            Fil.Delete(File);
        }
    }
}