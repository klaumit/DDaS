using System.Collections.Generic;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;
using DDaS.Core.API;
using DDaS.Core.Models;
using DDaS.Core.Tools;
using static DDaS.Core.Impl.ExeBased;
using static DDaS.Core.Tools.Defaults;

namespace DDaS.Core.Impl
{
    public sealed class GccIa16 : ICompiler
    {
        public async Task<IFileObj> CompileToAsm(IFileObj input)
        {
            List<string> args = ["-S"];
            return await Compile(input, args, SymExt, RunExe);
        }

        public async Task<IFileObj> CompileToCom(IFileObj input)
        {
            List<string> args = ["-o", input.GetNewName(ComExt)];
            return await Compile(input, args, ComExt, RunExe);
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