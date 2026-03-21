using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDaS.Core.Models;
using DDaS.Core.Tools;

namespace DDaS.Core.Impl
{
    internal static class ExeBased
    {
        internal static async Task<Compiled> Compile(IFileObj input,
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

            return new Compiled(new TempFile(file), (int)mil, cod, err);
        }
    }
}