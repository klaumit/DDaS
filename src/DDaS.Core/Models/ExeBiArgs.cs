using System.Collections.Generic;
using DDaS.IO.API;

namespace DDaS.Core.Models
{
    public sealed record ExeBiArgs : ExeArgs
    {
        private List<string> Items { get; }

        public ExeBiArgs(List<string> items)
        {
            Items = items;
        }

        public override void Add(IFile file) => Items.Add(file.Name);

        public override IEnumerable<string> Y => Items;
    }
}