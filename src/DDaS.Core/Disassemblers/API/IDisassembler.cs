using System.Threading.Tasks;
using DDaS.Core.Models;
using DDaS.IO.API;

namespace DDaS.Core.Disassemblers.API
{
    public interface IDisassembler
    {
        /// <summary>
        /// .com -> .asm
        /// </summary>
        Task<Executed> Disassemble(IFileX input);
    }
}