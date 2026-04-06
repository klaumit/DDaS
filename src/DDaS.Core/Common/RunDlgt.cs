using System.Collections.Generic;
using System.Threading.Tasks;
using CliWrap.Buffered;
using DDaS.IO.API;
using Microsoft.Extensions.Logging;

namespace DDaS.Core.Common
{
    internal delegate Task<BufferedCommandResult> RunDlgt(ILogger log, IDirX dir, IEnumerable<string> args);
}