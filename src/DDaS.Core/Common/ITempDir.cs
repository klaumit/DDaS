using System;
using System.IO;
using DDaS.Core.Models;

namespace DDaS.Core.Common
{
    public interface ITempDir : IDisposable
    {
        Stream NewStream(string name);

        IFileObj GetFileRef(string name);
        
        void TrackFile(string file);
    }
}