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
                switch (o.Action)
                {
                    case ActKind.Compile:
                        Actions.RunCompile(o);
                        return;
                }
            });
        }
    }
}