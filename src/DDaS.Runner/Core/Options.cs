using CommandLine;

// ReSharper disable ClassNeverInstantiated.Global

namespace DDaS.Runner.Core
{
    public class Options
    {
        [Option('a', "action", HelpText = "Specify action to run.")]
        public ActKind Action { get; set; }
    }
}