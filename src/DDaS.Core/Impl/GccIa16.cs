using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;
using DDaS.Core.API;
using DDaS.Core.Tools;

namespace DDaS.Core.Impl
{
    public sealed class GccIa16 : ICompiler
    {
        public async Task<IFileObj> Compile(IFileObj input)
        {
            var tmpDir = input.GetDirectoryOf();
            var batch = new[] { input };

            List<string> dArgs = ["-S"];
            Array.ForEach(batch, b => dArgs.Add(b.Name));

            var dumpCmd = await RunExe(tmpDir, dArgs);

            Array.ForEach(batch, b => b.Dispose());

            var error = dumpCmd.StandardError + dumpCmd.StandardOutput;
            if (!string.IsNullOrWhiteSpace(error) || dumpCmd.ExitCode != 0)
                throw new InvalidOperationException($"[{dumpCmd.ExitCode}] {error}");

            var baseName = Path.GetFileNameWithoutExtension(input.Name);
            var resFile = Path.Combine(tmpDir, $"{baseName}.s");
            return new TempFile(resFile);
        }

        private static async Task<BufferedCommandResult> RunExe(string root, IEnumerable<string> args)
        {
            const string cmd = "ia16-elf-gcc";
            var dumpCmd = await Cli.Wrap(cmd)
                .WithArguments(args)
                .WithWorkingDirectory(root)
                .WithValidation(CommandResultValidation.None)
                .ExecuteBufferedAsync();
            return dumpCmd;
        }
    }
}