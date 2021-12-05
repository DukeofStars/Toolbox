using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;
using System.Threading.Tasks;

using ToolBox.Main;
using ToolBox.Utilities;

namespace ToolBox.Apps
{
    public class Installer
    {
        private App App;
        public static string MainPath;
        private string AppMainPath;
        public static string TempPath;
        private static int amountOfInstallers;
        private LoadingForm progress;
        private event EventHandler InstallComplete;

        //public static List<(App, App)> updateQueue = new List<(App, App)>();

        public static void Install(App app)
        {
            Installer installer = new Installer(app);
            installer.progress = new LoadingForm();
            installer.SetProgressVisible(true);
            Task.Run(() => installer.Install());
            installer.Close();
        }

        public static void UnInstall(App app)
        {
            Installer installer = new Installer(app);
            installer.progress = new LoadingForm();
            installer.SetProgressVisible(true);
            installer.UnInstall();
            installer.Close();
        }

        public Installer(App app)
        {
            Installer.amountOfInstallers++;
            this.App = app;
            AppMainPath = Path.Combine(MainPath, this.App.Name);
            this.InstallComplete += this.Finished;
        }

        static Installer()
        {
            MainPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                "TsunamiSoftware");
            TempPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TsunamiSoftware", "temp\\");
        }

        public void UnInstall(bool showProgress = true)
        {
            try
            {
                // Delete Files
                if (showProgress)
                    progress.Invoke(new Action(() => BringProgressToFront()));
                    if (showProgress)
                        progress.Invoke(new Action(() => SetStatus("Checking for uninstall.exe")));
                    if (File.Exists(this.AppMainPath + @"\uninstall.exe"))
                        Process.Start(this.AppMainPath + @"\uninstall.exe");
                    if (showProgress)
                    {
                        progress.Invoke(new Action(() => Step()));
                        progress.Invoke(new Action(() => SetStatus("Deleting files")));
                    }
                    if (Directory.Exists(this.AppMainPath))
                        Directory.Delete(this.AppMainPath, true);
                    if (this.App.HasConfig && Directory.Exists(this.App.ConfigFolderPath))
                        Directory.Delete(this.App.ConfigFolderPath);
                    if (showProgress)
                        progress.Invoke(new Action(() => Step()));

                // Finish up
                if (showProgress)
                    progress.Invoke(new Action(() => SetStatus("Finishing")));
                this.App.Installed = false;
                if (showProgress)
                {
                    progress.Invoke(new Action(() => Step()));
                    progress.Invoke(new Action(() => InstallComplete(InstallResult.UNINSTALLED, null)));
                }
            }
            catch (UnauthorizedAccessException)
            {
                if (showProgress)
                    progress.Invoke(new Action(() => progress.Close()));
                Utilities.Utilities.RestartWithAdmin();
                return;
            }
        }

        public void Install(bool showProgress = true)
        {
            try
            {
                if (showProgress)
                    progress.Invoke(new Action(() => BringProgressToFront()));
                    if (!Directory.Exists(MainPath))
                        Directory.CreateDirectory(MainPath);
                if (showProgress)
                    progress.Invoke(new Action(() => Step()));

                // Download
                if (showProgress)
                    progress.Invoke(new Action(() => SetStatus("Downloading")));
                string tempZipPath = TempPath + @"\file" + Installer.amountOfInstallers + ".zip";
                if (!Directory.Exists(TempPath))
                    Directory.CreateDirectory(TempPath);
                WebClient webClient = new WebClient();
                webClient.DownloadFile(this.App.OnlineFilePath, tempZipPath);
                if (showProgress)
                    progress.Invoke(new Action(() => Step()));

                // Extract
                if (showProgress)
                    progress.Invoke(new Action(() => SetStatus("Extracting")));
                if (Directory.Exists(this.AppMainPath))
                    Directory.Delete(this.AppMainPath, true);
                Directory.CreateDirectory(this.AppMainPath);
                ZipFile.ExtractToDirectory(tempZipPath, this.AppMainPath);
                if (showProgress)
                    progress.Invoke(new Action(() => Step()));

                // Finish up
                if (showProgress)
                    progress.Invoke(new Action(() => SetStatus("Finishing")));
                Directory.Delete(TempPath, true);
                if (File.Exists(this.AppMainPath + "install.exe"))
                {
                    Process.Start(this.AppMainPath + "install.exe");
                }
                this.App.Installed = true;
                if (showProgress)
                {
                    progress.Invoke(new Action(() => Step()));
                    progress.Invoke(new Action(() => InstallComplete(InstallResult.INSTALLED, null)));
                }
            }
            catch (UnauthorizedAccessException)
            {
                if (showProgress)
                    progress.Invoke(new Action(() => SetProgressVisible(false)));
                Utilities.Utilities.RestartWithAdmin();
                return;
            }
        }

        public void Finished(object sender, EventArgs e)
        {
            if (sender is InstallResult.INSTALLED)
            {
                MessageBox.Show("Installation Complete");
                Header.SetPage(Page.UNINSTALLED);
            }
            else if (sender is InstallResult.UNINSTALLED)
            {
                MessageBox.Show("UnInstallation Complete");
                Header.SetPage(Page.INSTALLED);
            }
            else if (sender is InstallResult.UPDATE)
            {
                MessageBox.Show("Update Complete");
                Header.SetPage(Page.INSTALLED);
            }
            else if (sender is InstallResult.FAILED)
            {
                MessageBox.Show("Failed");
            }
            this.progress.Visible = false;
            this.progress.Enabled = false;
            this.progress.Dispose();
            MainWindow.self.InstalledPage.Refresh();
            MainWindow.self.UnInstalledPage.Refresh();
            MainWindow.self.Invalidate();
        }

        public void Close()
        {
            Installer.amountOfInstallers--;
        }

        private void SetStatus(string status)
        {
            this.progress.Status = status;
        }

        private void Step()
        {
            this.progress.Step();
        }

        private void SetProgressVisible(bool visible)
        {
            this.progress.Visible = visible;
            this.progress.Enabled = visible;
        }

        private void BringProgressToFront()
        {
            this.progress.BringToFront();
        }
    }

    enum InstallResult
    {
        INSTALLED,
        UNINSTALLED,
        UPDATE,
        FAILED
    }
}
