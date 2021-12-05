using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

using ToolBox.Apps;
using ToolBox.Main;
using ToolBox.Pages.InstalledPage;

namespace ToolBox.Pages.UnInstalledPage
{
    public class UnInstalledPanel : Panel
    {
        private List<AppButton> appButtons = new List<AppButton>();
        private List<App> apps = new List<App>();

        public bool HasPair(App app)
        {
            bool ret_val = false;
            foreach (App comp_app in MainWindow.self.InstalledPage.GetInstalledApps())
            {
                if (app.Name == comp_app.Name)
                    ret_val = true;
            }
            foreach (App comp_app in this.apps)
            {
                if (app.Name == comp_app.Name)
                    ret_val = true;
            }
            return ret_val;
        }

        public new void Refresh()
        {
            // Clear previous
            foreach (AppButton appButton in this.appButtons)
            {
                appButton.Visible = false;
                appButton.Enabled = false;
                appButton.Dispose();
            }

            // Load apps
            this.apps = new List<App>();
            this.appButtons = new List<AppButton>();
            foreach (App app in InstalledPanel.Apps)
            {
                if (!app.Installed /*&& !this.HasPair(app)*/)
                {
                    this.apps.Add(app);
                    AppButton appButton = new AppButton(app);
                    int x = (appButtons.Count % 3) * 200 + 100;
                    int y = (int)Math.Floor((double)appButtons.Count / 3) * 200 + 100;
                    appButton.Visible = true;
                    appButton.Enabled = true;
                    appButton.Location = new Point(x, y);
                    appButton.Parent = this;
                    Utilities.Utilities.RoundBorderControl(appButton);
                    appButton.BringToFront();
                    appButtons.Add(appButton);
                }
            }
            Update();
            base.Refresh();
        }

        public List<App> GetUninstalledApps()
        {
            return apps;
        }

        private List<App> ignored = new List<App>();

        public void Save()
        {
            Data.Store(this.apps);
        }
    }
}