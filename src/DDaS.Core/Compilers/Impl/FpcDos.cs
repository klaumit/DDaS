using System.Threading.Tasks;
using DDaS.Core.Models;
using System.Collections.Generic;
using CliWrap.Buffered;
using DDaS.Core.Compilers.API;
using static DDaS.Core.Common.ExeBased;
using static DDaS.Core.Tools.Defaults;
using E = DDaS.Core.Common.ExeBased;

namespace DDaS.Core.Compilers.Impl
{
    public sealed class FpcDos : ICompiler
    {
        public async Task<Executed> CompileToAsm(IFileObj input)
        {
            List<string> args = ["--help"];
            return await Compile(input, args, SymExt, RunExe);
        }

        public async Task<Executed> CompileToCom(IFileObj input)
        {
            List<string> args = ["-WmTiny", "-Wtcom"];
            return await Compile(input, args, ComExt, RunExe);
        }

        private static Task<BufferedCommandResult> RunExe(string root, IEnumerable<string> args)
        {
            return E.RunExe("ppcross8086", root, args);
        }
    }
}