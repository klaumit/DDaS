using System.Collections.Generic;
using System.Threading.Tasks;
using CliWrap.Buffered;
using Microsoft.Extensions.Logging;

namespace DDaS.Core.Common
{
    internal delegate Task<BufferedCommandResult> RunDlgt(ILogger log, string dir, IEnumerable<string> args);
}