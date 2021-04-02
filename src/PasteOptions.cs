using System;
using System.Text.RegularExpressions;
using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

using static TextCopy.Clipboard;

namespace p2u
{
    [Verb("pst", HelpText = "Paste the text content that you copied from the Internet.")]
    class PasteOptions
    {
        [Option('s', "silent", HelpText = "Run without print any feedback message.")]
        public bool Silent { get; set; }

        [Option('c', "cmd", HelpText = "Paste text content into cmd/DOS with Windows Terminal.")]
        public bool Cmd { get; set; }

        [Option('v', "vim", HelpText = "Paste text content into Vim with Windows Terminal.")]
        public bool Vim { get; set; }

        [Option('p', "powershell", HelpText = "Paste text content into Powershell with Windows Terminal.")]
        public bool Powershell { get; set; }

        [Option('b', "git-bash", HelpText = "Paste text content into Git Bash with Windows Terminal.")]
        public bool GitBash { get; set; }

        [Option('w', "wsl", HelpText = "Paste text content into WSL (Windows Subsystem for Linux) with Windows Terminal.", Default = true)]
        public bool Wsl { get; set; }

        [Usage(ApplicationAlias = "p2u")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Copy content from anywhere and run it before pasting into Wsl, which is the default.", new PasteOptions { Wsl = true });
                yield return new Example("And before pasting into Cmd without any feedback messages.", new PasteOptions { Cmd = true, Silent = true });
            }
        }

        public static int RunPasteAndReturnExitCode(PasteOptions options)
        {
            if (options.Cmd)
            {
                SetText(GetText().Replace("\\", "^"));

                if (!options.Silent)
                {
                    return Feedback(options);
                }
            }
            else if (options.Vim)
            {
                // Vim doesn't like white spaces on the left when in Git Bash with Windows Terminal ;)
                var content = Regex.Replace(GetText(), @"\s+", " ");
                SetText(content.Replace("\\ ", "\\\n"));

                if (!options.Silent)
                {
                    return Feedback(options);
                }
            }
            else
            {
                SetText(GetText().Replace(Environment.NewLine, "\n"));

                if (!options.Silent)
                {
                    return Feedback(options);
                }
            }

            return default;
        }

        private static int Feedback(PasteOptions options)
        {
            Console.WriteLine("Line endings converted!");

            var help = options.Powershell
                ? "Try to hit CTRL+V now..."
                : "Try to hit the right mouse button (or CTRL+SHIFT+V) now...";

            Console.WriteLine(help);

            if (options.Vim)
            {
                Console.WriteLine("Remember that Vim doesn't like white spaces on the left when in Git Bash with Windows Terminal ;)");
            }

            return 0;
        }
    }
}
