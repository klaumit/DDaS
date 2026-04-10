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
        private readonly ILoggerProvider _logProv;

        public Disassemblers(ILoggerProvider logProv)
        {
            _logProv = logProv;
        }

        public IDisassembler GetDisassembler(DisassembleId id)
        {
            var tName = GetType().FullName!.TrimEnd('s');
            var log = _logProv.CreateLogger($"{tName}<{id}>");
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