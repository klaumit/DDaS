using System.Threading.Tasks;
using DDaS.Core.Models;
using System.Collections.Generic;
using System.Linq;
using CliWrap.Buffered;
using DDaS.Core.Assemblers.API;
using DDaS.IO.API;
using DDaS.IO.Tools;
using Microsoft.Extensions.Logging;
using static DDaS.Core.Common.ExeBased;
using static DDaS.Core.Tools.Defaults;

namespace DDaS.Core.Assemblers.Impl
{
    public sealed class Fasm : IAssembler
    {
        private readonly ILogger _log;

        public Fasm(ILogger log)
        {
            _log = log;
        }

        public async Task<Executed> Assemble(IFile input)
        {
            List<string> args = [input.GetNewNamed(ComExt).Name];
            return await Compile(_log, input, args, ComExt, DoFasm);
        }

        private static Task<BufferedCommandResult> DoFasm(ILogger log, IDir root, IEnumerable<string> args)
            => RunExe(log, "fasm", root, args.Reverse());
    }
}