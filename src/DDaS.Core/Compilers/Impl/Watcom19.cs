using System.Threading.Tasks;
using DDaS.Core.Compilers.API;
using DDaS.Core.Models;
using DDaS.IO.API;
using Microsoft.Extensions.Logging;
using static DDaS.Core.Common.ExeBased;
using static DDaS.Core.Common.DosBased;
using static DDaS.Core.Tools.Defaults;

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
        private const string E = "WCC";

/*

#!/bin/sh

nasm -f obj s.asm -o s.obj

+++++++

wcc -1 -os -zls -zl -ms -s -d2 hello.c -fo=main.obj

+++++++

wlink @t.lnk option nodefaultlibs option start=_s_ option statics system t file {main.obj s.obj} name x.com

*/

        public async Task<Executed> CompileToAsm(IFile input)
            => await Compile(_log, input, [B, E, "-1", "-S"], AsmExt, RunExe);

        public async Task<Executed> CompileToCom(IFile input)
            => await Compile(_log, input, [B, E, "-1", "-mt", "-lt"], ComExt, RunExe);
    }
}
