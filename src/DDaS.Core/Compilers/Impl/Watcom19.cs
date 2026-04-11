using System;
using System.Threading.Tasks;
using DDaS.Core.Compilers.API;
using DDaS.Core.Models;
using DDaS.Core.Supplements;
using DDaS.IO.API;
using Microsoft.Extensions.Logging;
using DDaS.IO.Tools;
using static DDaS.Core.Common.ExeBased;
using static DDaS.Core.Common.DosBased;
using static DDaS.Core.Tools.Defaults;
using static DDaS.Core.Models.ExeArgs;

namespace DDaS.Core.Compilers.Impl
{
    public sealed class Watcom19 : ICompiler
    {
        private readonly ILogger _log;

        public Watcom19(ILogger log)
        {
            _log = log;
        }

        private const string B = @"D:\w19";
        private const string E1 = "nasm";
        private const string E2 = "wcc";
        private const string E3 = "wlink";

        public async Task<Executed> CompileToCom(IFile input)
        {
            var ia = input.Name;
            var ic = input.GetNewNamed(ComExt).Name;
            var sup = StaticSup.GetEmbeddedSet(CompileId.W19, "0");
            sup.CopyFor(input);
            var com = await Compile(_log, input, A(
                [B],
                [E1, "-f", "obj", "s.asm", "-o", "s.obj"],
                [E2, "-1", "-os", "-zls", "-zl", "-ms", "-s", "-d2", ia, "-fo=m.obj"],
                [E3, "@t.lnk", "system", "t", "file", "{m.obj", "s.obj}", "name", ic]
            ), ComExt, RunExe);
            return com;
        }

        public async Task<Executed> CompileToAsm(IFile input)
        {
            var ia = input.Name;
            var com = await Compile(_log, input, A(
                [B],
                [E2, "-1", "-os", "-zls", "-zl", "-ms", "-s", "-d2", ia, "-S", "-fo=m.asm"]
            ), AsmExt, RunExe);

            Environment.Exit(-1);
            return com;
        }
    }
}