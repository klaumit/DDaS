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
        Task<Executed> CompileToAsm(IFileX input);

        /// <summary>
        /// .c -> .com
        /// </summary>
        Task<Executed> CompileToCom(IFileX input);
    }
}