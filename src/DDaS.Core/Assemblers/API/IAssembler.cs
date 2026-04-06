using System.Threading.Tasks;
using DDaS.Core.Models;
using DDaS.IO.API;

namespace DDaS.Core.Assemblers.API
{
    public interface IAssembler
    {
        /// <summary>
        /// .asm -> .com
        /// </summary>
        Task<Executed> Assemble(IFile input);
    }
}