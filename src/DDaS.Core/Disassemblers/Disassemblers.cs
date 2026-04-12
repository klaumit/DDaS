using System;
using System.Collections.Generic;
using DDaS.Core.Disassemblers.API;
using DDaS.Core.Disassemblers.Impl;
using DDaS.Core.Models;
using DDaS.Core.Tools;
using Microsoft.Extensions.Logging;
using R = DDaS.Core.Resources.StaticRes;

namespace DDaS.Core.Disassemblers
{
    public sealed class Disassemblers : IDisassemblers
    {
        private readonly Dictionary<DisassembleId, ILogger> _logs;

        public Disassemblers(ILoggerFactory logs)
        {
            _logs = logs.CreateAll<DisassembleId>(GetType());
        }

        public IDisassembler GetDisassembler(DisassembleId id)
        {
            var log = _logs[id];
            return id switch
            {
                DisassembleId.NSM => new Nasm(log),
                DisassembleId.ICE => new Icey(log),
                DisassembleId.O16 => new ObjIa16(log),
                _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
            };
        }

        public IEnumerable<ToolInfo> ListDisassemblerInfo()
            => R.GetEmbeddedJson<ToolInfo[]>("disassemblers.json");
    }
}