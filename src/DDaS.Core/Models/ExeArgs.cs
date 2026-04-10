using System.Collections.Generic;
using System.Linq;

namespace DDaS.Core.Models
{
    public sealed record ExeArgs
    {
        private List<string> Items { get; }
        private bool NoAdd { get; }

        private ExeArgs(List<string> items, bool noAdd = false)
        {
            Items = items;
            NoAdd = noAdd;
        }

        public static ExeArgs A(params string[] args) => new List<string>(args);

        public static implicit operator ExeArgs(List<string> args) => new(args);

        public void Add(string text)
        {
            if (NoAdd)
                return;
            Items.Add(text);
        }

        public IEnumerable<string> Y => Items;
    }
}