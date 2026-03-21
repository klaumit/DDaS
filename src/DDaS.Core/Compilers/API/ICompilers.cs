using System.Collections.Generic;
using DDaS.Core.Models;

namespace DDaS.Core.Compilers.API
{
    public interface ICompilers
    {
        ICompiler GetCompiler(CompileId id);

        IEnumerable<ToolInfo> ListCompilerInfo();
    }
}