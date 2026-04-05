using System.IO;
using DDaS.Core.Models;
using Fil = System.IO.File;

namespace DDaS.Core.Tools
{
    public sealed class TempFile5 : IExFileObj
    {
        public string File { get; }
        public byte[] Bytes { get; }
        public string Mime { get; }

        public TempFile5(string file)
        {
            File = file;
            Bytes = FileTool.TryReadAllBytes(file) ?? [];
            Mime = Defaults.Octet;
        }

        public string Name => Path.GetFileName(File);

        public void Dispose()
        {
            if (!Fil.Exists(File))
                return;
            Fil.Delete(File);
        }
    }
}