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
        private readonly ILoggerProvider _logProv;

        public Compilers(ILoggerProvider logProv)
        {
            _logProv = logProv;
        }

        public ICompiler GetCompiler(CompileId id)
        {
            var tName = GetType().FullName!.TrimEnd('s');
            var log = _logProv.CreateLogger($"{tName}<{id}>");
            return id switch
            {
                CompileId.G16 => new GccIa16(log),
                CompileId.B20 => new BCpp20(log),
                CompileId.B30 => new BCpp30(log),
                CompileId.B31 => new BCpp31(log),
                CompileId.B45 => new BCpp45(log),
                CompileId.B52 => new BCpp52(log),
                CompileId.FPC => new FpcDos(log),
                CompileId.W19 => new Watcom19(log),
                _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
            };
        }

        public IEnumerable<ToolInfo> ListCompilerInfo()
            => R.GetEmbeddedJson<ToolInfo[]>("compilers.json");
    }
}