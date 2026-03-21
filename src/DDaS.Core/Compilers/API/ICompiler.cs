using System.Threading.Tasks;
using DDaS.Core.Models;

namespace DDaS.Core.API
{
    public interface ICompiler
    {
        Task<Executed> CompileToAsm(IFileObj input);

        Task<Executed> CompileToCom(IFileObj input);
    }
}