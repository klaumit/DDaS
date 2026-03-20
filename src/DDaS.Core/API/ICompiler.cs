using System.Threading.Tasks;
using DDaS.Core.Models;

namespace DDaS.Core.API
{
    public interface ICompiler
    {
        Task<IFileObj> CompileToAsm(IFileObj input);

        Task<IFileObj> CompileToCom(IFileObj input);
    }
}