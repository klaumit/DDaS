using System.Threading.Tasks;
using DDaS.Core.API;
using DDaS.Core.Models;
using static DDaS.Core.Impl.ExeBased;
using static DDaS.Core.Impl.DosBased;
using static DDaS.Core.Tools.Defaults;

namespace DDaS.Core.Impl
{
    public sealed class BCpp30 : ICompiler
    {
        private const string B = @"D:\b30";
        private const string E = "BCC";

        public async Task<Compiled> CompileToAsm(IFileObj input)
            => await Compile(input, [B, E, "-1", "-S"], AsmExt, RunExe);

        public async Task<Compiled> CompileToCom(IFileObj input)
            => await Compile(input, [B, E, "-1", "-mt", "-lt"], ComExt, RunExe);
    }
}