using System.Collections.Generic;
using System.Threading.Tasks;
using CliWrap.Buffered;
using DDaS.IO.API;
using Microsoft.Extensions.Logging;
using E = DDaS.Core.Common.ExeBased;

namespace DDaS.Core.Common
{
    internal static class DosBased
    {
        internal static async Task<BufferedCommandResult> RunExe(ILogger log, IDir root, IEnumerable<string> args)
        {
            var rest = string.Join(" ", args);
            var rArgs = new List<string> { "-quiet", "-dumb", "-d", ".", "-E", '"' + rest + '"' };
            var manual = string.Join(" ", rArgs);
            var res = await E.RunExe(log, "dosemu", root, manual: manual);
            root.TrackFiles("*.obj");
            return res;
        }
    }
}