using System.Threading.Tasks;
using DDaS.Core.Assemblers.API;
using DDaS.Core.Models;
using DDaS.IO.API;
using Microsoft.Extensions.Logging;
using static DDaS.Core.Common.ExeBased;
using static DDaS.Core.Common.DosBased;
using static DDaS.Core.Tools.Defaults;

namespace DDaS.Core.Assemblers.Impl
{
    public sealed class Masm : IAssembler
    {
        private readonly ILogger _log;

        public Masm(ILogger log)
        {
            _log = log;
        }

        private const string B = @"D:\m60";
        private const string E = "ML";

        public async Task<Executed> Assemble(IFile asm)
        {
            var com = await Compile(_log, asm, [B, E, "/AT"], ComExt, RunExe);
            return com;
        }
    }
}
