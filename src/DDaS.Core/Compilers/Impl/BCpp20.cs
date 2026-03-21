using System.Threading.Tasks;
using DDaS.Core.API;
using DDaS.Core.Models;
using static DDaS.Core.Compilers.Common.ExeBased;
using static DDaS.Core.Compilers.Common.DosBased;
using static DDaS.Core.Tools.Defaults;

namespace DDaS.Core.Compilers.Impl
{
    public sealed class BCpp20 : ICompiler
    {
        private const string B = @"D:\b20";
        private const string E = "BCC";

        public async Task<Executed> CompileToAsm(IFileObj input)
            => await Compile(input, [B, E, "-1", "-S"], AsmExt, RunExe);

        public async Task<Executed> CompileToCom(IFileObj input)
            => await Compile(input, [B, E, "-1", "-mt", "-lt"], ComExt, RunExe);
    }
}