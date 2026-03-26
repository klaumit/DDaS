using System.Collections.Generic;
using DDaS.Core.Models;

namespace DDaS.Core.Disassemblers.API
{
    public interface IDisassemblers
    {
        IDisassembler GetDisassembler(DisassembleId id);

        IEnumerable<ToolInfo> ListDisassemblerInfo();
    }
}