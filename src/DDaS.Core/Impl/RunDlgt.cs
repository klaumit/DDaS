using System.Collections.Generic;
using System.Threading.Tasks;
using CliWrap.Buffered;

namespace DDaS.Core.Impl
{
    internal delegate Task<BufferedCommandResult> RunDlgt(string dir, IEnumerable<string> args);
}