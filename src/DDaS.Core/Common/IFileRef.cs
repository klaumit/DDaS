using System;

namespace DDaS.Core.Common
{
    public interface IFileRef3 : IDisposable
    {
        byte[] Bytes { get; }
        
        string Name { get; }
        
        string Mime { get; }

        string GetNewName(string ext);
        
        string GetNewName(string suf, object tmpDir);
        
        string GetDirectoryOf();
    }
}