using System;
using DDaS.Core.API;

namespace DDaS.Core.Impl
{
    public sealed class Compilers : ICompilers
    {
        public ICompiler GetCompiler(CompileId id)
        {
            return id switch
            {
                CompileId.G16 => new GccIa16(),
                CompileId.B31 => new BCpp31(),
                _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
            };
        }
    }
}