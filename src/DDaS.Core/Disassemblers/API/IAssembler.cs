using System.Threading.Tasks;
using DDaS.Core.Models;

namespace DDaS.Core.Disassemblers.API
{
    public interface IDisassembler
    {
        /// <summary>
        /// .com -> .asm
        /// </summary>
        Task<Executed> Disassemble(IFileObj input);
    }
}