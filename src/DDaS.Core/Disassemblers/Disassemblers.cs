using System;
using System.Collections.Generic;
using DDaS.Core.Disassemblers.API;
using DDaS.Core.Disassemblers.Impl;
using DDaS.Core.Models;
using R = DDaS.Core.Resources.StaticRes;

namespace DDaS.Core.Disassemblers
{
    public sealed class Disassemblers : IDisassemblers
    {
        public IDisassembler GetDisassembler(DisassembleId id)
        {
            return id switch
            {
                DisassembleId.NSM => new Nasm(),
                _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
            };
        }

        public IEnumerable<ToolInfo> ListDisassemblerInfo()
            => R.GetEmbeddedJson<ToolInfo[]>("disassemblers.json", typeof(R));
    }
}