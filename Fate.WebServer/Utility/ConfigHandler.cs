using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;

namespace FateWebServer.Utility
{
    public class ConfigHandler
    {
        public string DatabaseServer { get; private set; }
        public uint DatabasePort { get; private set; }
        public string DatabaseUserName { get; private set; }
        public string DatabasePassword { get; private set; }
        public string DatabaseName { get; private set; }

        private string _configFilePath;
        private readonly List<string> _fileContent = new List<string>();
        public ConfigHandler(string configFilePath)
        {
            _configFilePath = configFilePath;
            if (File.Exists(configFilePath))
            {
                _fileContent = new List<string>(File.ReadAllLines(configFilePath));
            }
        }

        public bool IsConfigFileValid()
        {
            return _fileContent.Any();
        }

        public void LoadConfig()
        {
            uint parsedInt;
            DatabaseServer = GetConfigString("databaseserver");
            if (uint.TryParse(GetConfigString("databaseport"), out parsedInt))
            {
                DatabasePort = parsedInt;
            }
            DatabaseUserName = GetConfigString("databaseusername");
            DatabasePassword = GetConfigString("databasepassword");
            DatabaseName = GetConfigString("databasename");
        }

        private string GetConfigString(string key)
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
