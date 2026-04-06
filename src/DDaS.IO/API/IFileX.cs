using System.IO;

namespace DDaS.IO.API
{
    public interface IFileX : IEntryX
    {
        IDirX Dir { get; }

        Stream NewStream();
    }
}