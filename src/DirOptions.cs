using CommandLine;
using System;
using System.IO;
using Newtonsoft.Json.Linq;
using CommandLine.Text;
using System.Collections.Generic;

namespace p2u
{
    [Verb("dir", HelpText = "Set all profiles with the current folder location as the starting directory")]
    class DirOptions
    {
        [Value(index: 0, Required = true, HelpText = "Desired path (e.g. ${PWD})")]
        public string DesiredPath { get; set; }

        [Usage(ApplicationAlias = "p2u")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                yield return new Example("If the desired folder is not defined, the user profile home will be used", new DirOptions { DesiredPath = home });
            }
        }

        public static int RunDuplicateAndReturnExitCode(DirOptions options)
        {
            var localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var settingsPath = Path.Combine(localPath, @"Packages\Microsoft.WindowsTerminal_8wekyb3d8bbwe\LocalState\settings.json"); // TODO: Allow user to set it manually if needed

            var json = File.ReadAllText(settingsPath);
            var settings = JObject.Parse(json);

            var profiles = (JArray)settings["profiles"];

            for (int i = 0; i < profiles.Count; i++)
            {
                if (profiles[i]["startingDirectory"] == null)
                {
                    profiles[i].Last.AddAfterSelf(new JProperty("startingDirectory", options.DesiredPath));
                }
                else
                {
                    profiles[i]["startingDirectory"] = options.DesiredPath;
                }
            }

            File.WriteAllText(settingsPath, settings.ToString());
            Console.WriteLine("All done! Try to hit CTRL+SHIFT+D or ALT+Click the desired profile...");

            return 0;
        }
    }
}
