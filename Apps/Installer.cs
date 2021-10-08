using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

using ToolBox2.Main;

namespace ToolBox2.Apps
{
    public class Installer
    {
        private App App;
        private string MainPath;
        private string AppMainPath;
        private string TempPath;
        private static int amountOfInstallers;
        private LoadingForm progress;
        private event EventHandler InstallComplete;
        public static void Install(App app)
        {
            Installer installer = new Installer(app);
            installer.Install();
            installer.Close();
        }

        public static void UnInstall(App app)
        {
            Installer installer = new Installer(app);
            installer.UnInstall();
            installer.Close();
        }

        public Installer(App app)
        {
            Installer.amountOfInstallers++;
            this.App = app;
            this.MainPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                "StarSoft");
            this.AppMainPath = Path.Combine(this.MainPath, this.App.Name);
            this.TempPath = Path.Combine(this.MainPath, "temp");
            this.InstallComplete += this.Finished;
        }

        public void UnInstall()
        {
            // Initialize
            this.progress = new LoadingForm
            {
                Visible = true,
                Status = "Initializing",
                Enabled = true,
                Progress = 0,
                MaxLength = 3,
                StepLength = 1
            };
            this.progress.BringToFront();
            ThreadStart threadStart = new ThreadStart(() => this.RunUnInstall());
            Thread thread = new Thread(threadStart);
            thread.Name = "uninstallingthread" + Installer.amountOfInstallers;
            thread.Start();
        }

        public void Install()
        {
            // Initialize
            progress = new LoadingForm
            {
                Visible = true,
                Status = "Initializing",
                Enabled = true,
                Progress = 0,
                MaxLength = 4,
                StepLength = 1
            };
            progress.BringToFront();
            ThreadStart threadStart = new ThreadStart(() => this.RunInstall());
            Thread thread = new Thread(threadStart);
            thread.Name = "installingthread" + Installer.amountOfInstallers;
            thread.Start();
        }

        public void Finished(object sender, EventArgs e)
        {
            if (sender is InstallResult.INSTALLED)
            {
                MessageBox.Show("Install Complete");
                Header.SetPage(Page.UNINSTALLED);
            }
            else
            {
                MessageBox.Show("UnInstall Complete");
                Header.SetPage(Page.NULL);
                Header.SetPage(Page.INSTALLED);
            }
            this.progress.Visible = false;
            this.progress.Enabled = false;
            this.progress.Dispose();
            MainWindow.self.InstalledPage.Refresh();
            MainWindow.self.UnInstalledPage.Refresh();
            MainWindow.self.Update();
        }

        private void RunUnInstall()
        {
            // Delete Files
            this.progress.Invoke(new Action(() => this.BringProgressToFront()));
            try
            {
                this.progress.Invoke(new Action(() => this.SetStatus("Checking for uninstall.exe")));
                if (File.Exists(this.AppMainPath + @"\uninstall.exe"))
                    Process.Start(this.AppMainPath + @"\uninstall.exe");
                this.progress.Invoke(new Action(() => this.Step()));
                this.progress.Invoke(new Action(() => this.SetStatus("Deleting files")));
                if (Directory.Exists(this.AppMainPath))
                    Directory.Delete(this.AppMainPath, true);
                if (this.App.HasConfig && Directory.Exists(this.App.ConfigFolderPath))
                    Directory.Delete(this.App.ConfigFolderPath);
                this.progress.Invoke(new Action(() => this.Step()));
            }
            catch (UnauthorizedAccessException)
            {
                Util.Utilities.RestartWithAdmin();
                return;
            }

            // Finish up
            this.progress.Invoke(new Action(() => this.SetStatus("Finishing")));
            this.App.Installed = false;
            this.progress.Invoke(new Action(() => this.Step()));
            this.progress.Invoke(new Action(() => this.InstallComplete(InstallResult.UNINSTALLED, null)));
        }

        private void RunInstall()
        {
            this.progress.Invoke(new Action(() => this.BringProgressToFront()));
            try
            {
                if (!Directory.Exists(this.MainPath))
                    Directory.CreateDirectory(this.MainPath);
                DirectoryInfo info = new DirectoryInfo(this.MainPath);
                DirectorySecurity security = info.GetAccessControl();
                WindowsIdentity currentUserIdentity = WindowsIdentity.GetCurrent();
                FileSystemAccessRule fileSystemRule = new FileSystemAccessRule(
                    currentUserIdentity.Name,
                    FileSystemRights.Write,
                    InheritanceFlags.ObjectInherit |
                    InheritanceFlags.ContainerInherit,
                    PropagationFlags.None,
                    AccessControlType.Allow);
                security.AddAccessRule(fileSystemRule);
                info.SetAccessControl(security);
            }
            catch (UnauthorizedAccessException)
            {
                this.progress.Invoke(new Action(() => this.SetProgressVisible(false)));
                Util.Utilities.RestartWithAdmin();
                return;
            }
            this.progress.Invoke(new Action(() => this.Step()));

            // Download
            this.progress.Invoke(new Action(() => this.SetStatus("Downloading")));
            string tempZipPath = this.TempPath + @"\file" + Installer.amountOfInstallers + ".zip";
            if (!Directory.Exists(this.TempPath))
                Directory.CreateDirectory(this.TempPath);
            WebClient webClient = new WebClient();
            webClient.DownloadFile(this.App.OnlineFilePath, tempZipPath);
            this.progress.Invoke(new Action(() => this.Step()));

            // Extract
            this.progress.Invoke(new Action(() => this.SetStatus("Extracting")));
            if (Directory.Exists(this.AppMainPath))
                Directory.Delete(this.AppMainPath, true);
            Directory.CreateDirectory(this.AppMainPath);
            ZipFile.ExtractToDirectory(tempZipPath, this.AppMainPath);
            this.progress.Invoke(new Action(() => this.Step()));

            // Finish up
            this.progress.Invoke(new Action(() => this.SetStatus("Finishing")));
            Directory.Delete(this.TempPath, true);
            if (File.Exists(this.AppMainPath + "install.exe"))
            {
                Process.Start(this.AppMainPath + "install.exe");
            }
            this.App.Installed = true;
            this.progress.Invoke(new Action(() => this.Step()));
            this.progress.Invoke(new Action(() => this.InstallComplete(InstallResult.INSTALLED, null)));
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
        FAILED
    }
}
