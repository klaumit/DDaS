using System.Threading.Tasks;
using System.Collections.Generic;
using CliWrap;
using CliWrap.Buffered;

namespace DDaS.Core.Impl
{
    internal static class DosBased
    {
        internal static async Task<BufferedCommandResult> RunExe(string root, IEnumerable<string> args)
        {
            var rest = string.Join(" ", args);
            var rArgs = new List<string> { "-quiet", "-dumb", "-E", '"' + rest + '"' };
            var manual = string.Join(" ", rArgs);
            const string cmd = "dosemu";
            var dumpCmd = await Cli.Wrap(cmd)
                .WithArguments(manual)
                .WithWorkingDirectory(root)
                .WithValidation(CommandResultValidation.None)
                .ExecuteBufferedAsync();
            return dumpCmd;
        }
    }
}