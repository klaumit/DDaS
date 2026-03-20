using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDaS.Core.Models;
using DDaS.Core.Tools;

namespace DDaS.Core.Impl
{
    internal static class ExeBased
    {
        internal static async Task<IFileObj> Compile(IFileObj input,
            List<string> args, string suf, RunDlgt runExe)
        {
            var tmpDir = input.GetDirectoryOf();
            var batch = new[] { input };

            Array.ForEach(batch, b => args.Add(b.Name));

            var dumpCmd = await runExe(tmpDir, args);

            Array.ForEach(batch, b => b.Dispose());

            var error = dumpCmd.StandardError + dumpCmd.StandardOutput;
            if (!string.IsNullOrWhiteSpace(error) || dumpCmd.ExitCode != 0)
                throw new InvalidOperationException($"[{dumpCmd.ExitCode}] {error}");

            var resFile = input.GetNewName(suf, tmpDir);
            return new TempFile(resFile);
        }
    }
}