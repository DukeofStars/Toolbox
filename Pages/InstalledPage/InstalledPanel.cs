using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

using ToolBox.Main;
using ToolBox.Apps;

namespace ToolBox.Pages.InstalledPage
{
    public class InstalledPanel : Panel
    {
        private List<AppButton> appButtons = new List<AppButton>();
        private List<App> apps = new List<App>();

        public new void Refresh()
        {
            foreach (AppButton appButton in this.appButtons)
            {
                appButton.Visible = false;
                appButton.Enabled = false;
                appButton.Dispose();
            }
            this.apps = new List<App>();
            this.appButtons = new List<AppButton>();
            foreach (App app in InstalledPanel.Apps)
            {
                if (app.Installed)
                {
                    this.apps.Add(app);

                    AppButton appButton = new AppButton(app);
                    int x = (appButtons.Count % 4) * 200 + 100;
                    int y = (int)Math.Floor((double)appButtons.Count / 4) * 200 + 100;
                    appButton.Visible = true;
                    appButton.Enabled = true;
                    appButton.Location = new Point(x, y);
                    appButton.Parent = this;
                    Utilities.Utilities.RoundBorderControl(appButton);
                    appButton.BringToFront();
                    appButtons.Add(appButton);
                }
            }
        }

        public (bool, App) CheckForUpdates(App app)
        {
            foreach (App comp_app in InstalledPanel.Apps)
            {
                if (app.Name == comp_app.Name && app.Version < comp_app.Version)
                    return (true, comp_app);
            }
            return (false, null);
        }

        public List<App> GetInstalledApps()
        {
            return this.apps;
        }

        // Data

        public static List<App> Apps = new List<App>();

        public void Save()
        {
            Data.Store(this.apps);
        }
    }
}