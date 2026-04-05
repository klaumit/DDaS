using System;
using System.Collections.Generic;
using DDaS.Core.Disassemblers.API;
using DDaS.Core.Disassemblers.Impl;
using DDaS.Core.Models;
using Microsoft.Extensions.Logging;
using R = DDaS.Core.Resources.StaticRes;

namespace DDaS.Core.Disassemblers
{
    public sealed class Disassemblers : IDisassemblers
    {
        private readonly ILogger _log;

        public Disassemblers(ILogger log)
        {
            _log = log;
        }

        public IDisassembler GetDisassembler(DisassembleId id)
        {
            return id switch
            {
                DisassembleId.NSM => new Nasm(_log),
                DisassembleId.ICE => new Icey(_log),
                DisassembleId.O16 => new ObjIa16(_log),
                _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
            };
        }

        public IEnumerable<ToolInfo> ListDisassemblerInfo()
            => R.GetEmbeddedJson<ToolInfo[]>("disassemblers.json", typeof(R));
    }
}