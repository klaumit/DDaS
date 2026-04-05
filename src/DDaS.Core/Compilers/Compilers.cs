using System;
using System.Collections.Generic;
using DDaS.Core.Compilers.API;
using DDaS.Core.Compilers.Impl;
using DDaS.Core.Models;
using Microsoft.Extensions.Logging;
using R = DDaS.Core.Resources.StaticRes;

namespace DDaS.Core.Compilers
{
    public sealed class Compilers : ICompilers
    {
        private readonly ILogger _log;

        public Compilers(ILogger log)
        {
            _log = log;
        }
        
        public ICompiler GetCompiler(CompileId id)
        {
            return id switch
            {
                CompileId.G16 => new GccIa16(_log),
                CompileId.B20 => new BCpp20(_log),
                CompileId.B30 => new BCpp30(_log),
                CompileId.B31 => new BCpp31(_log),
                CompileId.FPC => new FpcDos(_log),
                _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
            };
        }

        public IEnumerable<ToolInfo> ListCompilerInfo()
            => R.GetEmbeddedJson<ToolInfo[]>("compilers.json", typeof(R));
    }
}