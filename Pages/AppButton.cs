﻿using System;
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
                
            }
            else
            {
                Installer.UnInstall(this.app);
                MainWindow.InstalledPanel.Refresh();
                MainWindow.UnInstalledPanel.Refresh();
                Header.SetPage(Page.INSTALLED);
                MainWindow.self.Invalidate();
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

        private void OpenAppPanel(object sender, MouseEventArgs e)
        {
            if (this.app.Installed)
            {
                AppDescPanel panel = new AppDescPanel();
                panel.Parent = MainWindow.self;
                panel.Location = new Point(1160, 0);
                panel.Name = this.app.Name;
                panel.Description = this.app.GetDescription();
                panel.Visible = true;
                panel.BringToFront();
                panel.Slide(Direction.LEFT);
                Header.SetPage(Page.APPDESC, panel);
                MainWindow.self.ClearPage();
            }
        }
    }
}
