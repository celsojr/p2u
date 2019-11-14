using CommandLine;

class Options
{
    [Option('s', "silent", Required = false, HelpText = "Run without print any feedback message", Default = false)]
    public bool Silent { get; set; }

    [Option('c', "cmd", Required = false, HelpText = "Paste text content into cmd/DOS with Windows Terminal", Default = false)]
    public bool Cmd { get; set; }

    [Option('v', "vim", Required = false, HelpText = "Paste text content into Vim with Windows Terminal", Default = false)]
    public bool Vim { get; set; }

    [Option('p', "powershell", Required = false, HelpText = "Paste text content into Powershell with Windows Terminal", Default = false)]
    public bool Powershell { get; set; }

    [Option('b', "git-bach", Required = false, HelpText = "Paste text content into Git Bash with Windows Terminal", Default = false)]
    public bool GitBash { get; set; }

    [Option('w', "wsl", Required = false, HelpText = "Paste text content into WSL - Windows Subsystem for Linux with Windows Terminal", Default = false)]
    public bool Wsl { get; set; }
}
