using System.Threading.Tasks;
using DDaS.Core.Models;
using System.Collections.Generic;
using CliWrap.Buffered;
using DDaS.Core.Assemblers.API;
using DDaS.IO.API;
using DDaS.IO.Tools;
using Microsoft.Extensions.Logging;
using static DDaS.Core.Common.ExeBased;
using static DDaS.Core.Tools.Defaults;

namespace DDaS.Core.Assemblers.Impl
{
    public sealed class Nasm : IAssembler
    {
        private readonly ILogger _log;

        public Nasm(ILogger log)
        {
            _log = log;
        }

        public async Task<Executed> Assemble(IFileX input)
        {
            List<string> args = ["-f", "bin", "-o", input.GetNewNamed(ComExt).Name];
            return await Compile(_log, input, args, ComExt, DoNasm);
        }

        private static Task<BufferedCommandResult> DoNasm(ILogger log, IDirX root, IEnumerable<string> args)
            => RunExe(log, "nasm", root, args);
    }
}