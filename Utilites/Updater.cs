using System.Collections.Generic;

using ToolBox.Pages.InstalledPage;
using ToolBox.Main;
using System;

namespace Toolbox.Update
{
    public class Updater
    {
        Dictionary<int, Version> appLatestVersions;
        Dictionary<int, Version> appLocalVersions;
        Dictionary<string, int>  appCodes;
        int lastCode = -1;

        public Updater()
        {
            appCodes = new Dictionary<string, int>();
        }

        void FetchLatest()
        {
            appLatestVersions = new Dictionary<int, Version>();
            foreach (var app in MainWindow.self.UnInstalledPage.GetUninstalledApps())
            {
                if (!appCodes.TryGetValue(app.Name, out int appCode))
                {
                    appCodes.Add(app.Name, lastCode++);
                    appCode = lastCode;
                }
                appLatestVersions.Add(appCode, app.Version);
            }
        }

        void FetchLocal()
        {
            appLocalVersions = new Dictionary<int, Version>();
            foreach (var app in InstalledPanel.Apps)
            {
                if (!appCodes.TryGetValue(app.Name, out int appCode))
                {
                    appCodes.Add(app.Name, lastCode++);
                    appCode = lastCode;
                }
                appLocalVersions.Add(appCode, app.Version);
            }
        }

        public void UpdateAll()
        {
            MainWindow.self.InitComplete += UpdateImpl;
        }

        void UpdateImpl(object sender, EventArgs e)
        {
            FetchLocal();
            FetchLatest();

            foreach (var localVersion in appLocalVersions)
            {
                if (appLatestVersions.TryGetValue(localVersion.Key, out Version version))
                {
                    if (version > localVersion.Value) System.Console.WriteLine("Update availible for " + localVersion.Key);
                }
            }
        }
    }
}
