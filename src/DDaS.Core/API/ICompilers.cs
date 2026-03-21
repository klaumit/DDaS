using System.Collections.Generic;
using DDaS.Core.Models;

namespace DDaS.Core.API
{
    public interface ICompilers
    {
        ICompiler GetCompiler(CompileId id);

        IEnumerable<ToolInfo> ListCompilerInfo();
    }
}