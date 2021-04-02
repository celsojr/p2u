using CommandLine;
using System;
using System.IO;
using Newtonsoft.Json.Linq;
using CommandLine.Text;
using System.Collections.Generic;

namespace p2u
{
    [Verb("dir", HelpText = "Set all profiles with the current folder location as the starting directory.")]
    class DirOptions
    {
        [Value(index: 0, Required = false, HelpText = "Desired path (e.g. ${PWD}).")]
        public string DesiredPath { get; set; }

        [Value(index: 1, Required = false, HelpText = "Fully qualified path to the Windows Terminal settings.json file.")]
        public string SettingsPath { get; set; }

        [Option('r', "reset", HelpText = "Reset to the Windows Terminal starting directory defaults which is \"%USERPROFILE%\".")]
        public bool Reset { get; set; }

        [Usage(ApplicationAlias = "p2u")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                yield return new Example("If the desired path is not defined, the user profile home will be used.", new DirOptions { DesiredPath = home });
            }
        }

        public static int RunDuplicateAndReturnExitCode(DirOptions options)
        {
            var localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var settingsPath = options.SettingsPath ??
                Path.Combine(localPath, @"Packages\Microsoft.WindowsTerminal_8wekyb3d8bbwe\LocalState\settings.json");

            var json = File.ReadAllText(settingsPath);
            var settings = JObject.Parse(json);

            var profiles = (JArray)settings["profiles"];

            for (int i = 0; i < profiles.Count; i++)
            {
                var startingDirectory = options.Reset
                    ? "%USERPROFILE%"
                    : options.DesiredPath ?? Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

                if (profiles[i]["startingDirectory"] == null)
                {
                    profiles[i].Last.AddAfterSelf(new JProperty("startingDirectory", startingDirectory));
                }
                else
                {
                    profiles[i]["startingDirectory"] = startingDirectory;
                }
            }

            File.WriteAllText(settingsPath, settings.ToString());
            Console.WriteLine("All done! Try to hit CTRL+SHIFT+D or ALT+Click the desired profile...");

            return 0;
        }
    }
}
