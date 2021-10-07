﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

using ToolBox2.Apps;
using ToolBox2.Main;
using ToolBox2.Pages.InstalledPage;

namespace ToolBox2.Pages.UnInstalledPage
{
    public class UnInstalledPanel : Panel
    {
        private List<AppButton> appButtons = new List<AppButton>();
        private List<App> apps = new List<App>();
        public UnInstalledPanel()
        {
            foreach (App app in InstalledPanel.Apps)
            {
                if (!app.Installed)
                {
                    this.apps.Add(app);
                    AppButton appButton = new AppButton(app);
                    int x = (appButtons.Count % 3) * 200 + 50;
                    int y = (int)Math.Floor((double)appButtons.Count / 3) * 200 + 100;
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
            Header.SetPage(Page.UNINSTALLED);
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
                if (!app.Installed)
                {
                    this.apps.Add(app);
                    AppButton appButton = new AppButton(app);
                    int x = (appButtons.Count % 3) * 200 + 50;
                    int y = (int)Math.Floor((double)appButtons.Count / 3) * 200 + 100;
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
            Header.SetPage(Page.UNINSTALLED);
        }

        public void Save()
        {
            Data.Store(this.apps);
        }
    }
}
