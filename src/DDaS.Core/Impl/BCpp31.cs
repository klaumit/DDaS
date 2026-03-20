using System.Threading.Tasks;
using DDaS.Core.API;
using DDaS.Core.Models;
using static DDaS.Core.Impl.ExeBased;
using static DDaS.Core.Impl.DosBased;
using static DDaS.Core.Tools.Defaults;

namespace DDaS.Core.Impl
{
    public sealed class BCpp31 : ICompiler
    {
        public async Task<IFileObj> CompileToAsm(IFileObj input)
            => await Compile(input, [@"D:\b31", "BCC", "-1", "-S"], AsmExt, RunExe);

        public async Task<IFileObj> CompileToCom(IFileObj input)
            => await Compile(input, [@"D:\b31", "BCC", "-1", "-mt", "-lt"], ComExt, RunExe);
    }
}