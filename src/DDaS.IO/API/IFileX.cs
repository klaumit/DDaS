using System.IO;

namespace DDaS.IO.API
{
    public interface IFileX : IEntryX
    {
        IDirX? Dir { get; }

        byte[] Bytes { get; }

        Stream NewStream();

        string Mime { get; }
    }
}