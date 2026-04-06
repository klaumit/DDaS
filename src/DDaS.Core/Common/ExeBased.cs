using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;
using DDaS.Core.Models;
using DDaS.IO.API;
using DDaS.IO.Temp;
using DDaS.IO.Tools;
using Microsoft.Extensions.Logging;

namespace DDaS.Core.Common
{
    internal static class ExeBased
    {
        internal static async Task<Executed> Compile(ILogger log, IFileX input,
            List<string> args, string suf, RunDlgt runExe)
        {
            var tmpDir = input.GetDirectoryOf();
            var batch = new[] { input };

            Array.ForEach(batch, b => args.Add(b.Name));

            var dumpCmd = await runExe(log, tmpDir, args);

            Array.ForEach(batch, b => b.Dispose());

            var err = dumpCmd.StandardError + '\n' + dumpCmd.StandardOutput;
            var cod = dumpCmd.ExitCode;
            var mil = dumpCmd.RunTime.TotalMilliseconds;
            var file = input.GetNewNamed(suf);

            return new Executed(file, (int)mil, cod, err);
        }

        public static async Task<BufferedCommandResult> RunExe(ILogger log, string exe,
            IDirX dir, IEnumerable<string>? args = null, string? manual = null)
        {
            var root = ((TmpDirX)dir).Real;
            
            var cmd = Cli.Wrap(exe)
                .WithWorkingDirectory(root)
                .WithValidation(CommandResultValidation.None);
            if (!string.IsNullOrWhiteSpace(manual))
                cmd = cmd.WithArguments(manual);
            if (args != null)
                cmd = cmd.WithArguments(args);
            if (log.IsEnabled(LogLevel.Debug))
                log.LogDebug("Executing '{Exe}' with '{Args}' in '{Dir}'...", exe, cmd.Arguments, cmd.WorkingDirPath);
            var res = await cmd.ExecuteBufferedAsync();
            if (log.IsEnabled(LogLevel.Debug))
                log.LogDebug("Executed '{Exe}' in {Run} with status {Code}!", exe, res.RunTime, res.ExitCode);
            return res;
        }
    }
}