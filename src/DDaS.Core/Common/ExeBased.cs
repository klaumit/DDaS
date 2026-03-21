using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;
using DDaS.Core.Models;
using DDaS.Core.Tools;

namespace DDaS.Core.Common
{
    internal static class ExeBased
    {
        internal static async Task<Executed> Compile(IFileObj input,
            List<string> args, string suf, RunDlgt runExe)
        {
            var tmpDir = input.GetDirectoryOf();
            var batch = new[] { input };

            Array.ForEach(batch, b => args.Add(b.Name));

            var dumpCmd = await runExe(tmpDir, args);

            Array.ForEach(batch, b => b.Dispose());

            var err = dumpCmd.StandardError + '\n' + dumpCmd.StandardOutput;
            var cod = dumpCmd.ExitCode;
            var mil = dumpCmd.RunTime.TotalMilliseconds;
            var file = input.GetNewName(suf, tmpDir);

            return new Executed(new TempFile(file), (int)mil, cod, err);
        }

        public static async Task<BufferedCommandResult> RunExe(string exe,
            string root, IEnumerable<string>? args = null, string? manual = null)
        {
            var cmd = Cli.Wrap(exe)
                .WithWorkingDirectory(root)
                .WithValidation(CommandResultValidation.None);
            if (!string.IsNullOrWhiteSpace(manual))
                cmd = cmd.WithArguments(manual);
            if (args != null)
                cmd = cmd.WithArguments(args);
            return await cmd.ExecuteBufferedAsync();
        }
    }
}