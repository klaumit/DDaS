using System.Threading.Tasks;
using DDaS.Core.API;

namespace DDaS.Core.Impl
{
    public sealed class BCpp20 : ICompiler
    {
        public Task<IFileObj> CompileToAsm(IFileObj input)
        {
            throw new System.NotImplementedException();
        }

        public Task<IFileObj> CompileToCom(IFileObj input)
        {
            throw new System.NotImplementedException();
        }
    }
}