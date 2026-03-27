using System.Threading.Tasks;
using DDaS.Core.Disassemblers.API;
using DDaS.Core.Models;

namespace DDaS.Core.Disassemblers.Impl
{
    public sealed class Icey : IDisassembler
    {
        public Task<Executed> Disassemble(IFileObj input)
        {
            throw new System.NotImplementedException();
        }
    }
}