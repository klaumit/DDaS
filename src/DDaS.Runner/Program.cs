using System;
using System.Threading.Tasks;
using CommandLine;
using DDaS.Runner.Core;

namespace DDaS.Runner
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var parser = Parser.Default;
            await parser.ParseArguments<Options>(args).WithParsedAsync(async o =>
            {
                Enum.TryParse<ActKind>(o.Action, true, out var act);
                switch (act)
                {
                    case ActKind.Compile:
                        Actions.RunCompile(o);
                        return;
                    case ActKind.Assemble:
                        await Actions.RunAssemble(o);
                        return;
                    case ActKind.Disassemble:
                        await Actions.RunDisassemble(o);
                        return;
                }
            });
        }
    }
}