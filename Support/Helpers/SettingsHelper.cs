using System;
using System.IO;

namespace Support.Helpers
{
    public static class SettingsHelper
    {
        public static string CheckAppWorkingDirectoryExist(string appName)
        {
            var pathToAppWorkingDirectory = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\{appName}";
            
            if (!Directory.Exists(pathToAppWorkingDirectory))
            {
                Directory.CreateDirectory(pathToAppWorkingDirectory);
            }

            return pathToAppWorkingDirectory;
        }
    }
}
