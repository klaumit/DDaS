using System.Threading.Tasks;
using DDaS.Core.Compilers.API;
using DDaS.Core.Models;
using DDaS.IO.API;
using Microsoft.Extensions.Logging;
using static DDaS.Core.Common.ExeBased;
using static DDaS.Core.Common.DosBased;
using static DDaS.Core.Tools.Defaults;

namespace DDaS.Core.Compilers.Impl
{
    public sealed class BCpp45 : ICompiler
    {
        private readonly ILogger _log;

        public BCpp45(ILogger log)
        {
            _log = log;
        }

        private const string B = @"D:\b45";
        private const string E = "BCC";

        public async Task<Executed> CompileToAsm(IFile input)
            => await Compile(_log, input, [B, E, "-1", "-S"], AsmExt, RunExe);

        public async Task<Executed> CompileToCom(IFile input)
            => await Compile(_log, input, [B, E, "-1", "-mt", "-lt"], ComExt, RunExe);
    }
}