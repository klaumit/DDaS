using System.Threading.Tasks;
using DDaS.Core.Disassemblers.API;
using DDaS.Core.Models;
using System.Collections.Generic;
using CliWrap.Buffered;
using DDaS.Core.Tools;
using DDaS.IO.API;
using Microsoft.Extensions.Logging;
using static DDaS.Core.Common.ExeBased;
using static DDaS.Core.Tools.Defaults;

namespace DDaS.Core.Disassemblers.Impl
{
    public sealed class ObjIa16 : IDisassembler
    {
        private readonly ILogger _log;

        public ObjIa16(ILogger log)
        {
            _log = log;
        }

        public async Task<Executed> Disassemble(IFile input)
        {
            List<string> args = ["-D", "-Mintel,i8086", "-b", "binary", "-m", "i386", "-z"];
            var exec = await Compile(_log, input, args, SymExt, DoDump);
            return await exec.MoveOutputToFile();
        }

        private static Task<BufferedCommandResult> DoDump(ILogger log, IDir root, IEnumerable<string> args)
            => RunExe(log, "ia16-elf-objdump", root, args);
    }
}