using System.Threading.Tasks;
using DDaS.Core.API;
using System;
using System.Collections.Generic;
using CliWrap;
using CliWrap.Buffered;
using DDaS.Core.Tools;

namespace DDaS.Core.Impl
{
    public abstract class DosBased
    {
        protected static async Task<IFileObj> Compile(IFileObj input, List<string> args, string suf)
        {
            var tmpDir = input.GetDirectoryOf();
            var batch = new[] { input };

            Array.ForEach(batch, b => args.Add(b.Name));

            var dumpCmd = await RunExe(tmpDir, args);

            Array.ForEach(batch, b => b.Dispose());

            var error = dumpCmd.StandardError;
            if (!string.IsNullOrWhiteSpace(error) || dumpCmd.ExitCode != 0)
                throw new InvalidOperationException($"[{dumpCmd.ExitCode}] {error}");

            var resFile = input.GetNewName(suf, tmpDir);
            return new TempFile(resFile);
        }

        private static async Task<BufferedCommandResult> RunExe(string root, IEnumerable<string> eArgs)
        {
            var rest = string.Join(" ", eArgs);
            var args = new List<string> { "-quiet", "-dumb", "-E", '"' + rest + '"' };
            var manual = string.Join(" ", args);
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