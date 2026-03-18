using System;
using System.Collections.Generic;
using System.Linq;
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
                CompileId.B20 => new BCpp20(),
                CompileId.B30 => new BCpp30(),
                CompileId.B31 => new BCpp31(),
                _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
            };
        }

        public IEnumerable<string> ListCompileIds()
        {
            var all = Enum.GetValues<CompileId>()
                .Except([default])
                .Select(x => x.ToString())
                .Order();
            return all;
        }
    }
}