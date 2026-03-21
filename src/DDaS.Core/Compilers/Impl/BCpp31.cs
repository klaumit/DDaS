using System.Threading.Tasks;
using DDaS.Core.Compilers.API;
using DDaS.Core.Models;
using static DDaS.Core.Common.ExeBased;
using static DDaS.Core.Common.DosBased;
using static DDaS.Core.Tools.Defaults;

namespace DDaS.Core.Compilers.Impl
{
    public sealed class BCpp31 : ICompiler
    {
        private const string B = @"D:\b31";
        private const string E = "BCC";

        public async Task<Executed> CompileToAsm(IFileObj input)
            => await Compile(input, [B, E, "-1", "-S"], AsmExt, RunExe);

        public async Task<Executed> CompileToCom(IFileObj input)
            => await Compile(input, [B, E, "-1", "-mt", "-lt"], ComExt, RunExe);
    }
}