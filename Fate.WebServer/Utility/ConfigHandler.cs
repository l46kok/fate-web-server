using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;

namespace FateWebServer.Utility
{
    public static class ConfigHandler
    {
        public static string DatabaseServer { get; private set; }
        public static uint DatabasePort { get; private set; }
        public static string DatabaseUserName { get; private set; }
        public static string DatabasePassword { get; private set; }
        public static string DatabaseName { get; private set; }
        public static string MapName { get; set; }

        private static string _configFilePath;
        private static List<string> _fileContent = new List<string>();

        public static bool IsConfigFileValid()
        {
            return _fileContent.Any();
        }

        public static void LoadConfig(string configFilePath)
        {
            _configFilePath = configFilePath;
            if (File.Exists(configFilePath))
            {
                _fileContent = new List<string>(File.ReadAllLines(configFilePath));
            }

            uint parsedInt;
            DatabaseServer = GetConfigString("databaseserver");
            if (uint.TryParse(GetConfigString("databaseport"), out parsedInt))
            {
                DatabasePort = parsedInt;
            }
            DatabaseUserName = GetConfigString("databaseusername");
            DatabasePassword = GetConfigString("databasepassword");
            DatabaseName = GetConfigString("databasename");
            MapName = GetConfigString("mapname");
        }

        private static string GetConfigString(string key)
        {
            foreach (string line in _fileContent)
            {
                if (String.IsNullOrEmpty(line))
                    continue;
                if (line[0] == '#') //Config Comment
                    continue;
                string[] configValueArray = line.Split('=');
                if (configValueArray.Length != 2)
                    continue;
                if (configValueArray[0] == key)
                    return configValueArray[1];
            }
            return String.Empty;
        }
    }
}
