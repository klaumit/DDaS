using System.Collections.Generic;
using System.Linq;

namespace DDaS.Core.Models
{
    public abstract record ExeArgs
    {
        public static ExeArgs A(params string[] a) => new List<string>(a);

        public static implicit operator ExeArgs(List<string> a) => new ExeBiArgs(a);

        public static ExeArgs A(params string[][] a) => a.Select(x => x.ToList()).ToArray();

        public static implicit operator ExeArgs(List<string>[] a) => new ExeBtArgs(a);

        public abstract void Add(string text);

        public abstract IEnumerable<string> Y { get; }
    }
}