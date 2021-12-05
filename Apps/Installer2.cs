using System.Net;
using System.IO;
using System.Diagnostics;
using System;

using ToolBox2.Utilites;
using ToolBox2.Util;

namespace ToolBox2.Apps
{
    internal class Installer2
    {
        public static void Install(App app)
        {
            try
            {
                Download(app);
                Run(app);
                CleanUp();
            }
            catch (UnauthorizedAccessException)
            {
                Utilities.RestartWithAdmin();
            }
        }

        static void Download(App app)
        {
            WebClient wc = new WebClient();
            wc.DownloadFile(app.OnlineFilePath, Path.Combine(FileManager.GetDirectory(Installer.TempPath), "installer.exe"));
            return;
        }

        static void Run(App app)
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
