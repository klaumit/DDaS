using System;
using CommandLine;
using DDaS.Runner.Core;

namespace DDaS.Runner
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var parser = Parser.Default;
            parser.ParseArguments<Options>(args).WithParsed(o =>
            {
                Enum.TryParse<ActKind>(o.Action, true, out var act);
                switch (act)
                {
                    case ActKind.Compile:
                        Actions.RunCompile(o);
                        return;
                    case ActKind.Assemble:
                        Actions.RunAssemble(o);
                        return;
                    case ActKind.Disassemble:
                        Actions.RunDisassemble(o);
                        return;
                }
            });
        }
    }
}