using System;
using System.Collections.Generic;
using DDaS.Core.Assemblers.API;
using DDaS.Core.Assemblers.Impl;
using DDaS.Core.Models;
using DDaS.Core.Tools;
using Microsoft.Extensions.Logging;
using R = DDaS.Core.Resources.StaticRes;

namespace DDaS.Core.Assemblers
{
    public sealed class Assemblers : IAssemblers
    {
        private readonly Dictionary<AssembleId, ILogger> _logs;

        public Assemblers(ILoggerFactory logs)
        {
            _logs = logs.CreateAll<AssembleId>(GetType());
        }

        public IAssembler GetAssembler(AssembleId id)
        {
            var log = _logs[id];
            return id switch
            {
                AssembleId.NSM => new Nasm(log),
                AssembleId.FSM => new Fasm(log),
                AssembleId.YSM => new Yasm(log),
                AssembleId.T50 => new Tasm(log),
                AssembleId.M60 => new Masm(log),
                _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
            };
        }

        public IEnumerable<ToolInfo> ListAssemblerInfo()
            => R.GetEmbeddedJson<ToolInfo[]>("assemblers.json");
    }
}