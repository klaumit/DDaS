using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;

namespace DDaS.Core
{
    public sealed class GccIa16 : ICompiler
    {
        private readonly string _tmpDir = FileTool.CreateOrGetDir("tmp_ia16")!;

        public string ExeName => "ia16-elf-gcc";

        public async Task<string> Compile(IEnumerable<byte[]> byteArrays)
        {
            var batch = byteArrays.Wrap(_tmpDir).ToArray();

            List<string> dArgs = ["-S"];
            Array.ForEach(batch, b => dArgs.Add(Path.GetRelativePath(_tmpDir, b.File)));

            var cmd = ExeName;
            var dumpCmd = await Cli.Wrap(cmd)
                .WithArguments(dArgs)
                .WithWorkingDirectory(_tmpDir)
                .WithValidation(CommandResultValidation.None)
                .ExecuteBufferedAsync();

            Array.ForEach(batch, b => b.Dispose());

            var error = dumpCmd.StandardError;
            if (!string.IsNullOrWhiteSpace(error) || dumpCmd.ExitCode != 0)
                throw new InvalidOperationException($"[{dumpCmd.ExitCode}] {error}");

            var stdOut = dumpCmd.StandardOutput;
            return stdOut;
        }
    }
}