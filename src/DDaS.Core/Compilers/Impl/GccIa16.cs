using System.Threading.Tasks;
using DDaS.Core.API;
using DDaS.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;
using DDaS.Core.API;
using DDaS.Core.Compilers.Common;
using DDaS.Core.Models;
using DDaS.Core.Tools;
using static DDaS.Core.Compilers.Common.ExeBased;
using static DDaS.Core.Tools.Defaults;

namespace DDaS.Core.Compilers.Impl
{
    public sealed class GccIa16 : ICompiler
    {
        public async Task<Executed> CompileToAsm(IFileObj input)
        {
            List<string> args = ["-S"];
            return await Compile(input, args, SymExt, RunExe);
        }

        public async Task<Executed> CompileToCom(IFileObj input)
        {
            List<string> args = ["-o", input.GetNewName(ComExt)];
            return await Compile(input, args, ComExt, RunExe);
        }

        private static Task<BufferedCommandResult> RunExe(string root, IEnumerable<string> args)
        {
            return ExeBased.RunExe("ia16-elf-gcc", root, args);
        }
    }
}