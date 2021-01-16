using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Fate.Common.Data.GHost;

namespace Fate.Common.Utility
{
    public static class ConfigHandler
    {
        public static uint WebServerPort { get; private set; }
        public static string DatabaseServer { get; private set; }
        public static uint DatabasePort { get; private set; }
        public static string DatabaseUserName { get; private set; }
        public static string DatabasePassword { get; private set; }
        public static string DatabaseName { get; private set; }
        public static string MapName { get; private set; }
        public static string ReplayFileLocation { get; private set; }

        public static List<GHostDatabaseInfo> GhostDatabaseList { get; } = new List<GHostDatabaseInfo>();
        public static List<GhostServerInfo> GHostServerList { get; } = new List<GhostServerInfo>();

        // ReSharper disable NotAccessedField.Local
        private static string _configFilePath;
        // ReSharper restore NotAccessedField.Local
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
            if (uint.TryParse(GetConfigString("webserverport"), out parsedInt))
            {
                WebServerPort = parsedInt;
            }

            DatabaseServer = GetConfigString("databaseserver");
            if (uint.TryParse(GetConfigString("databaseport"), out parsedInt))
            {
                DatabasePort = parsedInt;
            }
            DatabaseUserName = GetConfigString("databaseusername");
            DatabasePassword = GetConfigString("databasepassword");
            DatabaseName = GetConfigString("databasename");
            MapName = GetConfigString("mapname");
            ReplayFileLocation = GetConfigString("replayfilelocation");

            // Read database information first
            int databaseIdx = 1;
            while (true)
            {
                string databaseLocation = GetConfigString("ghost_databaselocation" + databaseIdx);
                if (String.IsNullOrEmpty(databaseLocation))
                    break;

                GHostDatabaseInfo ghostDbInfo = new GHostDatabaseInfo
                {
                    DatabaseLocation = databaseLocation,
                    DatabaseServer = GetConfigString("ghost_databaseserver" + databaseIdx),
                    DatabaseUserName = GetConfigString("ghost_databaseusername" + databaseIdx),
                    DatabasePassword = GetConfigString("ghost_databasepassword" + databaseIdx),
                    DatabaseName = GetConfigString("ghost_databasename" + databaseIdx)
                };

                if (uint.TryParse(GetConfigString("ghost_databaseport" + databaseIdx), out parsedInt))
                {
                    ghostDbInfo.DatabasePort = parsedInt;
                }

                GhostDatabaseList.Add(ghostDbInfo);
                databaseIdx++;
            }

            // Read server information second
            int serverIdx = 1;
            while (true)
            {

                string serverName = GetConfigString("ghost_servername" + serverIdx);
                if (String.IsNullOrEmpty(serverName))
                    break;
                GhostServerInfo ghostServerInfo = new GhostServerInfo
                {
                    ServerName = serverName,
                    IP = GetConfigString("ghost_serverIP" + serverIdx)
                };
                GHostServerList.Add(ghostServerInfo);
                serverIdx++;
            }
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
