using System.Threading.Tasks;
using DDaS.Core.API;
using DDaS.Core.Models;
using static DDaS.Core.Tools.Defaults;

namespace DDaS.Core.Impl
{
    public sealed class BCpp31 : DosBased, ICompiler
    {
        public async Task<IFileObj> CompileToAsm(IFileObj input)
        {
            return await Compile(input, [@"D:\b31", "BCC", "-1", "-S"], AsmExt);
        }

        public async Task<IFileObj> CompileToCom(IFileObj input)
        {
            return await Compile(input, [@"D:\b31", "BCC", "-1", "-mt", "-lt"], ComExt);
        }
    }
}