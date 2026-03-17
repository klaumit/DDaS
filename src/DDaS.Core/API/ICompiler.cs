using System.Threading.Tasks;

namespace DDaS.Core.API
{
    public interface ICompiler
    {
        Task<IFileObj> CompileToAsm(IFileObj input);

        Task<IFileObj> CompileToCom(IFileObj input);
    }
}