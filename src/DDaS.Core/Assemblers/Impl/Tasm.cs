using System.Threading.Tasks;
using DDaS.Core.Assemblers.API;
using DDaS.Core.Models;
using DDaS.IO.API;
using Microsoft.Extensions.Logging;
using static DDaS.Core.Common.ExeBased;
using static DDaS.Core.Common.DosBased;
using static DDaS.Core.Tools.Defaults;
using static DDaS.Core.Models.ExeArgs;

namespace DDaS.Core.Assemblers.Impl
{
    public sealed class Tasm : IAssembler
    {
        private readonly ILogger _log;

        public Tasm(ILogger log)
        {
            _log = log;
        }

        private const string B = @"D:\t50";
        private const string E1 = "TASM";
        private const string E2 = "TLINK";

        public async Task<Executed> Assemble(IFile input)
        {
            var com = await Compile(_log, input, A([B, E1], [B, E2, "/t"]), ComExt, RunExe);
            input.Dir?.TrackFiles("*.map");
            return com;
        }
    }
}