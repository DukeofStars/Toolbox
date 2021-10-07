using System;
using System.Drawing;
using System.Windows.Forms;

using ToolBox2.Apps;
using ToolBox2.Main;

namespace ToolBox2.Pages
{
    public partial class AppButton : UserControl
    {
        private App app;
        public AppButton(App app)
        {
            InitializeComponent();
            this.app = app;
            this.Name = this.app.Name;
            if (app.Installed)
            {
                this.btn_install.Image = Image.FromFile(@"icons\uninstall-delete.png");
            }
            this.btn_install.Click += this.Install;
        }

        private void Install(object sender, EventArgs e)
        {
            if (!this.app.Installed)
            {
                Installer.Install(this.app);
                MainWindow.InstalledPanel.Refresh();
                MainWindow.UnInstalledPanel.Refresh();
                Header.SetPage(Page.UNINSTALLED);
                MainWindow.self.Update();
            }
            else
            {
                Installer.UnInstall(this.app);
                MainWindow.InstalledPanel.Refresh();
                MainWindow.UnInstalledPanel.Refresh();
                Header.SetPage(Page.INSTALLED);
                MainWindow.self.Update();
            }
        }

        public new string Name
        {
            get
            {
                return this.AppNameLBL.Text;
            }
            set
            {
                this.AppNameLBL.Text = value;
            }
        }
    }
}
