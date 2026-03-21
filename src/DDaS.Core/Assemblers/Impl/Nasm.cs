using System.Threading.Tasks;
using DDaS.Core.Models;
using System.Collections.Generic;
using CliWrap.Buffered;
using DDaS.Core.Assemblers.API;
using DDaS.Core.Tools;
using static DDaS.Core.Common.ExeBased;
using static DDaS.Core.Tools.Defaults;

namespace DDaS.Core.Assemblers.Impl
{
    public sealed class Nasm : IAssembler
    {
        public async Task<Executed> Disassemble(IFileObj input)
        {
            List<string> args = ["-b", "16", "-p", "intel"];
            return await Compile(input, args, SymExt, DoDism);
        }

        public async Task<Executed> Assemble(IFileObj input)
        {
            List<string> args = ["-f", "bin", "-o", input.GetNewName(ComExt)];
            return await Compile(input, args, ComExt, DoNasm);
        }

        private static Task<BufferedCommandResult> DoDism(string root, IEnumerable<string> args)
        {
            return RunExe("ndisasm", root, args);
        }

        private static Task<BufferedCommandResult> DoNasm(string root, IEnumerable<string> args)
        {
            return RunExe("nasm", root, args);
        }
    }
}