using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
using System.Net;

using ToolBox.Apps;

namespace ToolBox.Main
{
    class Data
    {
        private static string FolderPath = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.ApplicationData), "TsunamiSoftware", "Toolbox");
        private static string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TsunamiSoftware", "Toolbox.config");
        
        private static Dictionary<string, App> Apps = new Dictionary<string, App>();

        public static List<App> AllApps;

        public static List<App> FetchInstalled()
        {
            List<App> installed;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<App>));
            if (!File.Exists(FilePath))
                File.Create(FilePath);
            using (var stream = File.OpenRead(Data.FilePath))
            {
                installed = (List<App>)xmlSerializer.Deserialize(stream);
            }
            return installed;
        }

        public static List<App> FetchAll()
        {
            // TODO: error in xml document

            List<App> all = new List<App>();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<App>));
            using (var stream = new WebClient().OpenRead(@"https://tsunamisoftware.netlify.app/internal/toolbox/apps.xml"))
            {
                all = (List<App>)xmlSerializer.Deserialize(stream);
            }
            return all;
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
