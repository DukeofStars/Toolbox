using ToolBox2.Apps;
using ToolBox2.Pages.InstalledPage;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
using System.Net;
using System.Windows.Forms;

namespace ToolBox2.Main
{
    class Data
    {
        private static string FolderPath = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.ApplicationData), "StarSoft", "Toolbox");
        private static string FilePath = Path.Combine(FolderPath, "Toolbox.config");
        private static Dictionary<string, App> Apps = new Dictionary<string, App>();
        private static Dictionary<string, App> AllApps = new Dictionary<string, App>();
        public static void InitializeData()
        {
            XmlSerializer serializer = new XmlSerializer(new List<App>().GetType());
            List<App> apps = new List<App>();
            try
            {
                // Get Installed Apps
                if (!File.Exists(Data.FilePath))
                    File.Create(Data.FilePath);
                using (var stream = File.OpenRead(Data.FilePath))
                {
                    apps = (List<App>)serializer.Deserialize(stream);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("An error occured when loading Apps");
            }
            try
            {
                // Get All Apps
                List<App> temp_;
                using (var stream = new WebClient().OpenRead(@"https://drive.google.com/uc?export=download&id=1_41S7QCoEz4MdCF4X6W5zj33oWFL0AOZ"))
                {
                    temp_ = (List<App>)serializer.Deserialize(stream);
                }
                foreach (App app in temp_)
                {
                    bool matched = false;
                    foreach (App comp_app in apps)
                    {
                        if (comp_app.Name == app.Name) matched = true;
                    }
                    if (matched) continue;
                    apps.Add(app);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("An error occured when loading Apps");
            }
            // Finish
            if (apps != null && apps.Count > 0)
            {
                InstalledPanel.Apps = apps;
            }
        }

        public static void Store(List<App> apps)
        {
            foreach(App app in apps)
            {
                if (Apps.ContainsKey(app.Name))
                    Apps[app.Name] = app;
                else
                    Apps.Add(app.Name, app);
            }
        }

        public static void Save()
        {
            XmlSerializer serializer = new XmlSerializer(new List<App>().GetType());
            if (!Directory.Exists(Data.FolderPath))
                Directory.CreateDirectory(Data.FolderPath);
            if (File.Exists(Data.FilePath))
                File.Delete(Data.FilePath);
            List<App> apps = new List<App>();
            foreach (App app in Data.Apps.Values)
                if (app.Installed) 
                    apps.Add(app);
            using (var stream = File.OpenWrite(Data.FilePath))
                serializer.Serialize(stream, apps);
        }
    }
}
