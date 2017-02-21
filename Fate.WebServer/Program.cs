using System;
using Fate.DB;
using FateWebServer.Utility;
using Nancy.Hosting.Self;

namespace FateWebServer
{
    static class Program
    {
        private const ushort WEB_SERVER_PORT = 64402;
        private const string TERMINATE_STRING = "/Terminate";
        private const string MAINTENANCE_STRING = "/Maintenance";
        private const string DEFAULT_CONFIG_FILE_PATH = "config.cfg";

        static void Main()
        {
            ConfigHandler configHandler = new ConfigHandler(DEFAULT_CONFIG_FILE_PATH);
            if (!configHandler.IsConfigFileValid())
                Console.WriteLine("Error loading default config file: {0}", DEFAULT_CONFIG_FILE_PATH);
            else
            {
                Console.WriteLine("Loading default config file: {0}", DEFAULT_CONFIG_FILE_PATH);
                configHandler.LoadConfig();
                frsDb.InitDatabaseConnection(configHandler.DatabaseServer,
                                             configHandler.DatabasePort,
                                             configHandler.DatabaseUserName,
                                             configHandler.DatabasePassword,
                                             configHandler.DatabaseName);
            }

            #region Making new instance of NancyHost
            var uri = new Uri("http://localhost:" + WEB_SERVER_PORT + "/");
            var config = new HostConfiguration
            {
                UrlReservations = {CreateAutomatically = true},
                AllowChunkedEncoding = false
            };

            var host = new NancyHost(config, uri);
            #endregion
            #region NancyFX hosting loop

            try
            {
                host.Start();

                Console.Write("Fate/Another III Ranking System Web Server\n" +
                              "\t\"" + uri + "\"\n" +
                              "To quit, input \"" + TERMINATE_STRING + "\".\n\n" +
                              "To set maintenance mode, input \"" + MAINTENANCE_STRING + "\".\n\n");
                bool terminateServer = false;
                while (!terminateServer)
                {
                    Console.Write("> ");
                    string cmd = Console.ReadLine();
                    switch (cmd)
                    {
                        case TERMINATE_STRING:
                            terminateServer = true;
                            break;
                        case MAINTENANCE_STRING:
                            MainBootstrapper.IsMaintenanceMode = !MainBootstrapper.IsMaintenanceMode;
                            if (MainBootstrapper.IsMaintenanceMode)
                                Console.WriteLine("Starting maintenance mode");
                            else
                                Console.WriteLine("Resuming web service");
                            break;

                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Unhandled exception has been occured!\n"
                    + e.Message);
                Console.ReadKey(true);
            }
            finally
            {
                host.Stop();
            }

            Console.WriteLine("Goodbye");
            #endregion
        }
    }
}
