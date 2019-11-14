using System;
using CommandLine;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace p2u
{
    using static Environment;
    using static TextCopy.Clipboard;

    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<Options>(args);
            await result.MapResult(async o => await Run(o), MapErrors);
        }

        private static Task MapErrors(IEnumerable<Error> errors)
        {
            foreach (var error in errors)
            {
                Console.WriteLine(error.ToString());
            }

            return Task.CompletedTask;
        }

        private static async Task Run(Options options)
        {
            if (options.Cmd)
            {
                SetText(GetText().Replace("\\", "^"));

                if (!options.Silent)
                {
                    await Console.Out.WriteLineAsync($"Line endings converted!{NewLine}Try to hit the right mouse button now (or SHIFT+CTRL+V)...");
                }
            }
            else if (options.Vim)
            {
                var content = Regex.Replace(GetText(), @"\s+", " ");
                SetText(content.Replace("\\ ", "\\\n"));

                if (!options.Silent)
                {
                    await Console.Out.WriteLineAsync("Line endings converted! Try to hit the right mouse button now (or SHIFT+CTRL+V)...");
                }
            }
            else if (options.Powershell)
            {
                SetText(GetText().Replace(NewLine, "\n"));

                if (!options.Silent)
                {
                    await Console.Out.WriteLineAsync("Line endings converted! Try to hit CTRL+V now...");
                }
            }
            else if (options.GitBash || options.Wsl)
            {
                SetText(GetText().Replace(NewLine, "\n"));

                if (!options.Silent)
                {
                    await Console.Out.WriteLineAsync("Line endings converted! Try to hit the right mouse button now (or SHIFT+CTRL+V)...");
                }
            }
            
        }
    }
}
