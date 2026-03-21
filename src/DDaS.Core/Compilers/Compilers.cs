using System;
using System.Collections.Generic;
using DDaS.Core.API;
using DDaS.Core.Compilers.Impl;
using DDaS.Core.Models;
using R = DDaS.Core.Resources.StaticRes;

namespace DDaS.Core.Compilers
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

        public IEnumerable<ToolInfo> ListCompilerInfo()
            => R.GetEmbeddedJson<ToolInfo[]>("compilers.json", typeof(R));
    }
}