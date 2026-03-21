using System.Threading.Tasks;
using DDaS.Core.Models;

namespace DDaS.Core.API
{
    public interface ICompiler
    {
        /// <summary>
        /// .c -> .asm
        /// </summary>
        Task<Executed> CompileToAsm(IFileObj input);

        /// <summary>
        /// .c -> .com
        /// </summary>
        Task<Executed> CompileToCom(IFileObj input);
    }
}