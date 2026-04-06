using DDaS.Core.Common;

namespace DDaS.Core.Models
{
    public interface IExFileObj : IFileObj
    {
        string File { get; }
        
        ITempDir? Folder { get; }
    }
}