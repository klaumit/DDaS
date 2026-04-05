using System.Collections.Generic;
using System.Threading.Tasks;
using CliWrap.Buffered;
using Microsoft.Extensions.Logging;
using E = DDaS.Core.Common.ExeBased;

namespace DDaS.Core.Common
{
    internal static class DosBased
    {
        internal static Task<BufferedCommandResult> RunExe(ILogger log, string root, IEnumerable<string> args)
        {
            var rest = string.Join(" ", args);
            var rArgs = new List<string> { "-quiet", "-dumb", "-E", '"' + rest + '"' };
            var manual = string.Join(" ", rArgs);
            return E.RunExe(log, "dosemu", root, manual: manual);
        }
    }
}