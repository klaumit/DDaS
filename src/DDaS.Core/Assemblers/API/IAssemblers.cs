using System.Collections.Generic;
using DDaS.Core.Models;

namespace DDaS.Core.Assemblers.API
{
    public interface IAssemblers
    {
        IAssembler GetAssembler(AssembleId id);

        IEnumerable<ToolInfo> ListAssemblerInfo();
    }
}