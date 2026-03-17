using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;
using DDaS.Core.API;
using DDaS.Core.Tools;
using static DDaS.Core.API.Defaults;

namespace DDaS.Core.Impl
{
    public sealed class GccIa16 : ICompiler
    {
        public async Task<IFileObj> CompileToAsm(IFileObj input)
        {
            List<string> args = ["-S"];
            return await Compile(input, args, SymbolExt);
        }

        public async Task<IFileObj> CompileToCom(IFileObj input)
        {
            List<string> args = ["-o", input.GetNewName(ComExt)];
            return await Compile(input, args, ComExt);
        }

        private static async Task<IFileObj> Compile(IFileObj input, List<string> args, string suf)
        {
            var tmpDir = input.GetDirectoryOf();
            var batch = new[] { input };

            Array.ForEach(batch, b => args.Add(b.Name));

            var dumpCmd = await RunExe(tmpDir, args);

            Array.ForEach(batch, b => b.Dispose());

            var error = dumpCmd.StandardError + dumpCmd.StandardOutput;
            if (!string.IsNullOrWhiteSpace(error) || dumpCmd.ExitCode != 0)
                throw new InvalidOperationException($"[{dumpCmd.ExitCode}] {error}");

            var resFile = input.GetNewName(suf, tmpDir);
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