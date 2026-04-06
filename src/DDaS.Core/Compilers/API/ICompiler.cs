using System.Threading.Tasks;
using DDaS.Core.Models;
using DDaS.IO.API;

namespace DDaS.Core.Compilers.API
{
    public interface ICompiler
    {
        /// <summary>
        /// .c -> .asm
        /// </summary>
        Task<Executed> CompileToAsm(IFile input);

        /// <summary>
        /// .c -> .com
        /// </summary>
        Task<Executed> CompileToCom(IFile input);
    }
}