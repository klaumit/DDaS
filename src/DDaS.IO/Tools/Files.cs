using System;
using System.IO;
using DDaS.IO.API;
using DDaS.IO.Memory;
using DDaS.IO.Temp;

namespace DDaS.IO.Tools
{
    public static class Files
    {
        public static IFile NewTmpFile(string name, byte[] bytes, string mime = Mimes.OctFile)
        {
            var hash = $"{Random.Shared.Next():x4}";
            var folder = Path.Combine("tmp", hash);
            var label = Path.GetFileName(name);
            var dir = new TmpDir(folder);
            var file = ((TmpFile)dir.GetFile(label)).WriteTo(bytes);
            file.Mime = mime;
            return file;
        }

        public static IFile NewMemFile(string name, byte[] bytes, string mime = Mimes.OctFile)
        {
            var hash = $"{Random.Shared.Next():x4}";
            var label = Path.GetFileName(name);
            var dir = new MemDir(hash);
            var file = ((MemFile)dir.GetFile(label)).WriteTo(bytes);
            file.Mime = mime;
            return file;
        }
    }
}