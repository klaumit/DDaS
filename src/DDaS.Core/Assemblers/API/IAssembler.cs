using System.Threading.Tasks;
using DDaS.Core.Models;

namespace DDaS.Core.Assemblers.API
{
    public interface IAssembler
    {
        /// <summary>
        /// .com -> .asm
        /// </summary>
        Task<Executed> Disassemble(IFileObj input);

        /// <summary>
        /// .asm -> .com
        /// </summary>
        Task<Executed> Assemble(IFileObj input);
    }
}