using System.Threading.Tasks;
using DDaS.Core.Models;

namespace DDaS.Core.API
{
    public interface ICompiler
    {
        Task<Compiled> CompileToAsm(IFileObj input);

        Task<Compiled> CompileToCom(IFileObj input);
    }
}