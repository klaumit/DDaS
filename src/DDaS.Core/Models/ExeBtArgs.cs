using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DDaS.Core.Models
{
    public sealed record ExeBtArgs : ExeArgs
    {
        public ExeBtArgs(List<string>[] lists)
        {
            throw new NotImplementedException(JsonConvert.SerializeObject(lists));
        }

        /*
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
        */

        public override void Add(string text)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<string> Y { get; }
    }
}