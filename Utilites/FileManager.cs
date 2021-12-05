using System.IO;

namespace ToolBox.Utilities
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
