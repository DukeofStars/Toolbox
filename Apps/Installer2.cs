using System.Net;
using System.IO;
using System.Diagnostics;
using System;

using ToolBox.Main;
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

                // Finish
                app.Installed = true;

                MainWindow.self.Invalidate();
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e);
            }
        }

        public static void Uninstall(App app)
        {
            string path1 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "TsunamiSoftware", app.Name, "unins000.exe");
            if (File.Exists(path1))
                Process.Start(path1).WaitForExit();
            string path2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "TsunamiSoftware", app.Name, "unins001.exe");
            if (File.Exists(path2))
                Process.Start(path2).WaitForExit();

            app.Installed = false;

            MainWindow.self.Invalidate();
        }

        static void Download(App app)
        {
            WebClient wc = new WebClient();
            wc.DownloadFile(app.OnlineFilePath, Path.Combine(FileManager.GetDirectory(Installer.TempPath), "installer.exe"));
            return;
        }

        static void Run()
        {
            Process.Start(Path.Combine(FileManager.GetDirectory(Installer.TempPath), "installer.exe")).Exited += CleanUp;
            return;
        }

        static void CleanUp(object sender, EventArgs e)
        {
            Directory.Delete(Installer.TempPath, true);
            return;
        }
    }
}
