using System.Collections.Generic;

using ToolBox2.Pages.InstalledPage;

namespace Toolbox2.Update
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
            FetchLocal();
            FetchLatest();
        }
    }
}
