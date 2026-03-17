using System.IO;
using DDaS.Core.API;
using Fil = System.IO.File;

namespace DDaS.Core.Tools
{
    public sealed class TempFile : IFileObj
    {
        public string File { get; }
        public byte[] Bytes { get; }

        public TempFile(string file)
        {
            File = file;
            Bytes = Fil.ReadAllBytes(file);
        }

        public TempFile(string dir, byte[] bytes, char prefix = 'a')
            : this(FileTool.WriteNewFile(dir, bytes, prefix))
        {
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