using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

class Options
{
    [Option('s', "silent", HelpText = "Run without print any feedback message")]
    public bool Silent { get; set; }

    [Option('c', "cmd", HelpText = "Paste text content into cmd/DOS with Windows Terminal")]
    public bool Cmd { get; set; }

    [Option('v', "vim", HelpText = "Paste text content into Vim with Windows Terminal")]
    public bool Vim { get; set; }

    [Option('p', "powershell", HelpText = "Paste text content into Powershell with Windows Terminal")]
    public bool Powershell { get; set; }

    [Option('b', "git-bash", HelpText = "Paste text content into Git Bash with Windows Terminal")]
    public bool GitBash { get; set; }

    [Option('w', "wsl", HelpText = "Paste text content into WSL (Windows Subsystem for Linux) with Windows Terminal", Default = true)]
    public bool Wsl { get; set; }

    [Usage(ApplicationAlias = "p2u")]
    public static IEnumerable<Example> Examples
    {
        get
        {
            yield return new Example("Copy content from anywhere and run it before pasting into Wsl, which is the default", new Options { Wsl = true });
            yield return new Example("And before pasting into Cmd without any feedback messages", new Options { Cmd = true, Silent = true });
        }
    }
}
