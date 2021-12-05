using System.IO;

namespace ToolBox2.Utilites
{
    internal class FileManager
    {
        public static string GetDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }
    }
}
