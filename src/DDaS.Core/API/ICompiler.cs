using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDaS.Core
{
    public interface ICompiler
    {
        Task<string> Compile(IEnumerable<byte[]> byteArrays);
    }
}