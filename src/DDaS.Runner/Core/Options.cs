using CommandLine;

// ReSharper disable ClassNeverInstantiated.Global

namespace DDaS.Runner.Core
{
    public class Options
    {
        [Option('a', "action", HelpText = "Specify action to run.")]
        public string? Action { get; set; }

        [Option('k', "kind", HelpText = "Specify kind to use.")]
        public string? Kind { get; set; }

        [Option('i', "input", HelpText = "Input file to read.")]
        public string? InputFile { get; set; }
    }
}