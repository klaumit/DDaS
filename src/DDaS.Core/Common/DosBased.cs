using System.Collections.Generic;
using System.Threading.Tasks;
using CliWrap.Buffered;
using E = DDaS.Core.Common.ExeBased;

namespace DDaS.Core.Common
{
    internal static class DosBased
    {
        internal static Task<BufferedCommandResult> RunExe(string root, IEnumerable<string> args)
        {
            var rest = string.Join(" ", args);
            var rArgs = new List<string> { "-quiet", "-dumb", "-E", '"' + rest + '"' };
            var manual = string.Join(" ", rArgs);
            return E.RunExe("dosemu", root, manual: manual);
        }
    }
}