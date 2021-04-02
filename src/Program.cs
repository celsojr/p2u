using System;
using CommandLine;

namespace p2u
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new Parser(with => with.HelpWriter = Console.Out);
            var parserResult = parser.ParseArguments<PasteOptions, DirOptions>(args);

            parserResult
                .WithParsed<PasteOptions>(opt => PasteOptions.RunPasteAndReturnExitCode(opt))
                .WithParsed<DirOptions>(dopt => DirOptions.RunDuplicateAndReturnExitCode(dopt));
        }
    }
}
