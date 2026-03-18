using System.Collections.Generic;

namespace DDaS.Core.API
{
    public interface ICompilers
    {
        ICompiler GetCompiler(CompileId id);

        IEnumerable<string> ListCompileIds();
    }
}