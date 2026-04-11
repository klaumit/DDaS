using System.Collections.Generic;

namespace DDaS.Core.Models
{
    public sealed record ExeBiArgs : ExeArgs
    {
        private List<string> Items { get; }

        public ExeBiArgs(List<string> items)
        {
            Items = items;
        }

        public override void Add(string text) => Items.Add(text);

        public override IEnumerable<string> Y => Items;
    }
}