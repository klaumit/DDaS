using System;
using System.IO;
using DDaS.IO.API;
using DDaS.IO.Memory;
using DDaS.IO.Temp;

namespace DDaS.IO.Tools
{
    public static class Files
    {
        public static IFileX NewMemFile(string path, byte[] bytes, string mime, IDirX tmp)
        {
            var file = new MemFileX(path, tmp).WriteTo(bytes);
            file.Mime = mime;
            return file;
        }

        public static IDirX NewTmpDir()
        {
            var hash = $"{Random.Shared.Next():x4}";
            return new TmpDirX(Path.Combine("tmp", hash));
        }
    }
}