using System.Threading.Tasks;
using DDaS.Core.Models;
using System.Collections.Generic;
using CliWrap.Buffered;
using DDaS.Core.Compilers.API;
using DDaS.IO.API;
using DDaS.IO.Tools;
using Microsoft.Extensions.Logging;
using static DDaS.Core.Common.ExeBased;
using static DDaS.Core.Tools.Defaults;
using E = DDaS.Core.Common.ExeBased;

namespace DDaS.Core.Compilers.Impl
{
    public sealed class GccIa16 : ICompiler
    {
        private readonly ILogger _log;

        public GccIa16(ILogger log)
        {
            _log = log;
        }

        public async Task<Executed> CompileToAsm(IFile input)
        {
            List<string> args = ["-S"];
            return await Compile(_log, input, args, SymExt, RunExe);
        }

        public async Task<Executed> CompileToCom(IFile input)
        {
            List<string> args = ["-o", input.GetNewNamed(ComExt).Name];
            return await Compile(_log, input, args, ComExt, RunExe);
        }

        private static Task<BufferedCommandResult> RunExe(ILogger log, IDir root, IEnumerable<string> args)
            => E.RunExe(log, "ia16-elf-gcc", root, args);
    }
}