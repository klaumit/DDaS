using System.IO;

namespace DDaS.IO.API
{
    public interface IFile : IEntry
    {
        IDir? Dir { get; }

        byte[] Bytes { get; }

        Stream NewStream();

        string Mime { get; }
    }
}