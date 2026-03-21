using System.Collections.Generic;
using System.Threading.Tasks;
using CliWrap.Buffered;

namespace DDaS.Core.Common
{
    internal delegate Task<BufferedCommandResult> RunDlgt(string dir, IEnumerable<string> args);
}