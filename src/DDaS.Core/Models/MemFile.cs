namespace DDaS.Core.Models
{
    public record MemFile(string Name, byte[] Bytes, string Mime) : IFileObj
    {
        public void Dispose()
        {
        }
    }
}