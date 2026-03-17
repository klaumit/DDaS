using System.Threading.Tasks;

namespace DDaS.Core.API
{
    public interface ICompiler
    {
        Task<IFileObj> Compile(IFileObj input);
    }
}