using System;
using System.IO;

namespace ToolBox.Apps
{
    public class App
    {
        public string Name { get; set; }
        public string OnlineFilePath { get; set; }
        public string ConfigFolderPath { get; set; }
        public string ConfigFilePath { get; set; }
        public Toolbox.Update.Version Version { get; set; }
        public bool Installed { get; set; }
        public bool HasConfig { get; set; }

        public string GetDescription()
        {
            string infoPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                "TsunamiSoftware", this.Name, "info.txt");
            if (File.Exists(infoPath))
            {
                return File.ReadAllText(infoPath);
            }
            else return "Could not find info.txt file for this app";
        }
    }
}
