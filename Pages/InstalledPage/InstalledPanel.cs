using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

using ToolBox2.Main;
using ToolBox2.Apps;
using ToolBox2.Util;

namespace ToolBox2.Pages.InstalledPage
{
    public class InstalledPanel : Panel
    {
        private List<AppButton> appButtons = new List<AppButton>();
        private List<App> apps = new List<App>();
        public void Initialize()
        {
            //MessageBox.Show("amount of apps = " + this.apps.Count + ", amount of set apps = " + InstalledPanel.Apps.Count);
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
                    Util.Utilities.RoundBorderControl(appButton);
                    appButton.BringToFront();
                    appButtons.Add(appButton);
                }
            }
            Header.SetPage(Page.NULL);
            Header.SetPage(Page.INSTALLED);
        }

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
                    Util.Utilities.RoundBorderControl(appButton);
                    appButton.BringToFront();
                    appButtons.Add(appButton);
                }
            }
            Header.SetPage(Page.NULL);
            Header.SetPage(Page.INSTALLED);
        }

        // Data

        public static List<App> Apps = new List<App>();

        public void Save()
        {
            Data.Store(this.apps);
        }
    }
}