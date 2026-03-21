using System;
using System.Collections.Generic;
using DDaS.Core.Assemblers.API;
using DDaS.Core.Assemblers.Impl;
using DDaS.Core.Models;
using R = DDaS.Core.Resources.StaticRes;

namespace DDaS.Core.Assemblers
{
    public sealed class Assemblers : IAssemblers
    {
        public IAssembler GetAssembler(AssembleId id)
        {
            return id switch
            {
                AssembleId.NSM => new Nasm(),
                _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
            };
        }

        public IEnumerable<ToolInfo> ListAssemblerInfo()
            => R.GetEmbeddedJson<ToolInfo[]>("assemblers.json", typeof(R));
    }
}