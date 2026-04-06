using System.Threading.Tasks;
using DDaS.Core.Models;
using System.Collections.Generic;
using CliWrap.Buffered;
using DDaS.Core.Disassemblers.API;
using DDaS.Core.Tools;
using DDaS.IO.API;
using Microsoft.Extensions.Logging;
using static DDaS.Core.Common.ExeBased;
using static DDaS.Core.Tools.Defaults;

namespace DDaS.Core.Disassemblers.Impl
{
    public sealed class Nasm : IDisassembler
    {
        private readonly ILogger _log;

        public Nasm(ILogger log)
        {
            _log = log;
        }

        public async Task<Executed> Disassemble(IFile input)
        {
            List<string> args = ["-b", "16", "-p", "intel"];
            var exec = await Compile(_log, input, args, SymExt, DoDism);
            return await exec.MoveOutputToFile();
        }

        private static Task<BufferedCommandResult> DoDism(ILogger log, IDir root, IEnumerable<string> args)
            => RunExe(log, "ndisasm", root, args);
    }
}