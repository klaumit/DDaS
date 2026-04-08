using System;
using System.Collections.Generic;
using DDaS.Core.Assemblers.API;
using DDaS.Core.Assemblers.Impl;
using DDaS.Core.Models;
using Microsoft.Extensions.Logging;
using R = DDaS.Core.Resources.StaticRes;

namespace DDaS.Core.Assemblers
{
    public sealed class Assemblers : IAssemblers
    {
        private readonly ILoggerProvider _logProv;

        public Assemblers(ILoggerProvider logProv)
        {
            _logProv = logProv;
        }

        public IAssembler GetAssembler(AssembleId id)
        {
            var tName = GetType().FullName!.TrimEnd('s');
            var log = _logProv.CreateLogger($"{tName}<{id}>");
            return id switch
            {
                AssembleId.NSM => new Nasm(log),
                AssembleId.FSM => new Fasm(log),
                AssembleId.YSM => new Yasm(log),
                AssembleId.T50 => new Tasm(log),
                _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
            };
        }

        public IEnumerable<ToolInfo> ListAssemblerInfo()
            => R.GetEmbeddedJson<ToolInfo[]>("assemblers.json", typeof(R));
    }
}