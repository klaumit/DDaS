using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDaS.IO.API;
using DDaS.IO.Tools;

namespace DDaS.Core.Models
{
    public sealed record ExeBtArgs : ExeArgs
    {
        public const string Mark = "script.bat";

        private readonly List<string>[] _lists;
        private readonly List<string> _args;

        public ExeBtArgs(List<string>[] lists)
        {
            _lists = lists;
            _args = [];
        }

        public override IEnumerable<string> Y => _args;

        public override void Add(IFile file)
        {
            var dir = file.GetDirectoryOf();
            var exe = ToBatch(_lists.Take(1));
            var cnt = new[] { new List<string> { "@echo off" } }.Concat(_lists.Skip(1));
            var txt = ToBatch(cnt);
            var bat = dir.GetFile(Mark).WriteTo(txt);
            _args.Clear();
            _args.Add(exe.Trim());
            _args.Add(bat.Name);
        }

        private static string ToBatch(IEnumerable<List<string>> lists)
        {
            var bld = new StringBuilder();
            foreach (var list in lists)
            {
                var line = string.Join(" ", list);
                bld.AppendLine(line);
            }
            return bld.ToString();
        }
    }
}