using System;
using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

namespace p2u
{
    class Program
    {
        public static void Main(string[] args)
        {
            var parser = new Parser(with => with.HelpWriter = null);
            var parserResult = parser.ParseArguments<PasteOptions, DirOptions>(args);

            parserResult
                .WithParsed<PasteOptions>(opt => PasteOptions.RunPasteAndReturnExitCode(opt))
                .WithParsed<DirOptions>(dopt => DirOptions.RunDuplicateAndReturnExitCode(dopt))
                .WithNotParsed(errs => DisplayHelp(parserResult, errs));
        }

        static void DisplayHelp<T>(ParserResult<T> result, IEnumerable<Error> errs)
        {
            var helpText = HelpText.AutoBuild(result, h =>
            {
                h.AdditionalNewLineAfterOption = false;
                h.Heading = "p2u 1.0.2";
                h.Copyright = "Copyright (c) 2021 Celso Jr";
                h.AutoVersion = false;
                h.MaximumDisplayWidth = 200;
                return HelpText.DefaultParsingErrorsHandler(result, h);
            }, e => e);
            Console.WriteLine(helpText);
        }

    }
}
