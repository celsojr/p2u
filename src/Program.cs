using System;
using CommandLine;
using CommandLine.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace p2u
{
    using static Environment;
    using static TextCopy.Clipboard;

    internal class Program
    {
        public static void Main(string[] args)
        {
            var parser = new CommandLine.Parser(with => with.HelpWriter = null);
            var parserResult = parser.ParseArguments<Options>(args);

            parserResult
                .WithParsed<Options>(async options => await Run(options))
                .WithNotParsed(errs => DisplayHelp(parserResult, errs));
        }

        static void DisplayHelp<T>(ParserResult<T> result, IEnumerable<Error> errs)
        {
            var helpText = HelpText.AutoBuild(result, h =>
            {
                h.AdditionalNewLineAfterOption = false;
                h.Heading = "p2u 1.0.1";
                h.Copyright = "Copyright (c) 2019 Celso Jr";
                h.AutoVersion = false;
                h.MaximumDisplayWidth = 200;
                return HelpText.DefaultParsingErrorsHandler(result, h);
            }, e => e);
            Console.WriteLine(helpText);
        }

        private static async Task Run(Options options)
        {
            if (options.Cmd)
            {
                SetText(GetText().Replace("\\", "^"));

                if (!options.Silent)
                {
                    await Feedback(options);
                }
            }
            else if (options.Vim)
            {
                // Vim doesn't like white spaces on the left when in Git Bash with Windows Terminal ;)
                var content = Regex.Replace(GetText(), @"\s+", " ");
                SetText(content.Replace("\\ ", "\\\n"));

                if (!options.Silent)
                {
                    await Feedback(options);
                }
            }
            else
            {
                SetText(GetText().Replace(NewLine, "\n"));

                if (!options.Silent)
                {
                    await Feedback(options);
                }
            }
        }

        private static async Task Feedback(Options options)
        {
            await Console.Out.WriteLineAsync("Line endings converted!");

            var help = options.Powershell
                ? "Try to hit CTRL+V now..."
                : "Try to hit the right mouse button (or CTRL+SHIFT+V) now...";

            await Console.Out.WriteLineAsync(help);

            if (options.Vim)
            {
                await Console.Out.WriteLineAsync("Remember that Vim doesn't like white spaces on the left when in Git Bash with Windows Terminal ;)");
            }
        }
    }
}
