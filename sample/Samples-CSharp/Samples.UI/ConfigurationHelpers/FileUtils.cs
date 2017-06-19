using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples.UI.ConfigurationHelpers
{
    public class FileUtils
    {
        public static string GetConfigurationFilesRootPath()
        {
            return Environment.CurrentDirectory + @"\ConfigurationFiles\";
        }

        public static string ResolveSampleConfigFilePath(string groupName, string configFileName)
        {
            string path = GetConfigurationFilesRootPath() + Path.Combine(groupName, configFileName);
            return path;
        }





       

    }
}
