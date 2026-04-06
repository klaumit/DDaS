using System;
using System.IO;
using DDaS.IO.API;
using DDaS.IO.Memory;
using DDaS.IO.Temp;

namespace DDaS.IO.Tools
{
    public static class Files
    {
        public static IFile NewMemFile(string path, byte[] bytes, string mime, IDir tmp)
        {
            var file = new MemFile(path, tmp).WriteTo(bytes);
            file.Mime = mime;
            return file;
        }

        public static IDir NewTmpDir()
        {
            var hash = $"{Random.Shared.Next():x4}";
            return new TmpDir(Path.Combine("tmp", hash));
        }
    }
}