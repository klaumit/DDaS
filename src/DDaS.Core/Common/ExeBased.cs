using System;
using System.Collections.Generic;
using System.Linq;
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
        internal static async Task<Executed> Compile(ILogger log, IFile input,
            ExeArgs args, string suf, RunDlgt runExe)
        {
            var tmpDir = input.GetDirectoryOf();
            var batch = new[] { input };

            Array.ForEach(batch, args.Add);

            var dumpCmd = await runExe(log, tmpDir, args.Y);

            Array.ForEach(batch, b => b.Dispose());

            var err = dumpCmd.StandardError + '\n' + dumpCmd.StandardOutput;
            var cod = dumpCmd.ExitCode;
            var mil = dumpCmd.RunTime.TotalMilliseconds;
            var file = input.GetNewNamed(suf);

            return new Executed(file, (int)mil, cod, err);
        }

        public static async Task<BufferedCommandResult> RunExe(ILogger log, string exe,
            IDir dir, IEnumerable<string>? args = null, string? manual = null)
        {
            var root = ((TmpDir)dir).Real;

            var cmd = Cli.Wrap(exe)
                .WithWorkingDirectory(root)
                .WithValidation(CommandResultValidation.None);
            if (!string.IsNullOrWhiteSpace(manual))
                cmd = cmd.WithArguments(manual);
            if (args != null)
                cmd = cmd.WithArguments(args);
            if (log.IsEnabled(LogLevel.Debug))
                log.LogDebug("Executing '{Exe}' with '{Args}' in '{Dir}'...", exe, A(cmd, dir), cmd.WorkingDirPath);
            var res = await cmd.ExecuteBufferedAsync();
            if (log.IsEnabled(LogLevel.Debug))
                log.LogDebug("Executed '{Exe}' in {Run} with status {Code}!", exe, res.RunTime, res.ExitCode);
            return res;
        }

        private static string A(Command cmd, IDir dir)
        {
            var args = cmd.Arguments;
            const string mf = ExeBtArgs.Mark;
            if (args.Contains(mf))
            {
                args = args.Replace(mf, string.Empty).TrimEnd('"');
                _ = dir.GetFile(mf).ReadTxt(out var txt);
                var lines = txt.Split('\n')
                    .SkipWhile(l => l.StartsWith('@'))
                    .Where(l => !string.IsNullOrWhiteSpace(l))
                    .Select(l => l.Trim());
                args += string.Join(" && ", lines);
                if (args.Contains(" \"")) args += '"';
            }
            return args;
        }
    }
}