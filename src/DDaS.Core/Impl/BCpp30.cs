using System.Threading.Tasks;
using DDaS.Core.API;
using DDaS.Core.Models;
using DDaS.Core.Tools;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;
using DDaS.Core.API;
using DDaS.Core.Models;
using DDaS.Core.Tools;
using static DDaS.Core.Impl.ExeBased;
using static DDaS.Core.Impl.DosBased;
using static DDaS.Core.Tools.Defaults;

namespace DDaS.Core.Impl
{
    public sealed class BCpp30 : ICompiler
    {
        public async Task<IFileObj> CompileToAsm(IFileObj input)
            => await Compile(input, [@"D:\b30", "BCC", "-1", "-S"], AsmExt, RunExe);

        public async Task<IFileObj> CompileToCom(IFileObj input)
            => await Compile(input, [@"D:\b30", "BCC", "-1", "-mt", "-lt"], ComExt, RunExe);
    }
}