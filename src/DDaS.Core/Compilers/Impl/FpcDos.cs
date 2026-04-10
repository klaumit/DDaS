using System.Threading.Tasks;
using DDaS.Core.Models;
using System.Collections.Generic;
using CliWrap.Buffered;
using DDaS.Core.Compilers.API;
using DDaS.Core.Tools;
using DDaS.IO.API;
using DDaS.IO.Tools;
using Microsoft.Extensions.Logging;
using static DDaS.Core.Common.ExeBased;
using static DDaS.Core.Tools.Defaults;
using E = DDaS.Core.Common.ExeBased;

namespace DDaS.Core.Compilers.Impl
{
    public sealed class FpcDos : ICompiler
    {
        private readonly ILogger _log;

        public FpcDos(ILogger log)
        {
            _log = log;
        }

        public async Task<Executed> CompileToAsm(IFile input)
        {
            List<string> args = ["-WmTiny", "-Wtcom", "-al", "-st", "-Anasm"];
            var exec = await Compile(_log, input, args, SymExt, RunExe);
            return await exec.CollectScattered(SymExt, "%LINE ");
        }

        public async Task<Executed> CompileToCom(IFile input)
        {
            List<string> args = ["-WmTiny", "-Wtcom"];
            var exec = await Compile(_log, input, args, ComExt, RunExe);
            input.GetDirectoryOf().TrackFiles("*.a");
            return exec;
        }

        private static Task<BufferedCommandResult> RunExe(ILogger log, IDir root, IEnumerable<string> args)
            => E.RunExe(log, "ppcross8086", root, args);
    }
}