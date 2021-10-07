using System;
using System.IO;

namespace ToolBox2.Apps
{
    public class App
    {
        public string Name { get; set; }
        public bool HasConfig { get; set; }
        public string OnlineFilePath { get; set; }
        public string ConfigFolderPath { get; set; }
        public string ConfigFilePath { get; set; }
        public bool Installed { get; set; }

        public string GetDescription()
        {
            string infoPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                "StarSoft", this.Name, "info.txt");
            if (File.Exists(infoPath))
            {
                return File.ReadAllText(infoPath);
            }
            else return "%%NULL%%";
        }
    }
}
