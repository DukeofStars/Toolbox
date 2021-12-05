using System.Net;
using System.IO;
using System.Diagnostics;
using System;

using ToolBox.Utilities;

namespace ToolBox.Apps
{
    internal class Installer2
    {
        public static void Install(App app)
        {
            try
            {
                Download(app);
                Run();
                CleanUp();
            }
            catch (UnauthorizedAccessException)
            {
                Utilities.Utilities.RestartWithAdmin();
            }
        }

        static void Download(App app)
        {
            WebClient wc = new WebClient();
            wc.DownloadFile(app.OnlineFilePath, Path.Combine(FileManager.GetDirectory(Installer.TempPath), "installer.exe"));
            return;
        }

        static void Run()
        {
            Process.Start(Path.Combine(FileManager.GetDirectory(Installer.TempPath), "installer.exe"));
            return;
        }

        static void CleanUp()
        {
            Directory.Delete(Installer.TempPath, true);
            return;
        }
    }
}
