using System;
using Fate.DB;
using FateWebServer.Utility;
using Nancy.Hosting.Self;
using NLog;

namespace FateWebServer
{
    static class Program
    {
        private const ushort WEB_SERVER_PORT = 64402;
        private const string TERMINATE_STRING = "/Terminate";
        private const string MAINTENANCE_STRING = "/Maintenance";
        private const string RELOAD_CONFIG_STRING = "/ReloadConfig";
        private const string DEFAULT_CONFIG_FILE_PATH = "config.cfg";
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        static void Main()
        {
            LoadConfig();
            RunServer();
        }

        private static void LoadConfig()
        {
            ConfigHandler.LoadConfig(DEFAULT_CONFIG_FILE_PATH);
            if (!ConfigHandler.IsConfigFileValid())
                _logger.Error("Error loading default config file: {0}", DEFAULT_CONFIG_FILE_PATH);
            else
            {
                _logger.Info("Loading default config file: {0}", DEFAULT_CONFIG_FILE_PATH);
                frsDb.InitDatabaseConnection(ConfigHandler.DatabaseServer,
                                             ConfigHandler.DatabasePort,
                                             ConfigHandler.DatabaseUserName,
                                             ConfigHandler.DatabasePassword,
                                             ConfigHandler.DatabaseName);
            }
        }

        private static void RunServer()
        {
            var uri = new Uri("http://localhost:" + WEB_SERVER_PORT + "/");
            var config = new HostConfiguration
            {
                UrlReservations = { CreateAutomatically = true },
                AllowChunkedEncoding = false
            };

            var host = new NancyHost(config, uri);

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
                            _logger.Info(MainBootstrapper.IsMaintenanceMode
                                ? "Starting maintenance mode"
                                : "Resuming web service");
                            break;
                        case RELOAD_CONFIG_STRING:
                            LoadConfig();
                            break;

                    }
                }

            }
            catch (Exception e)
            {
                _logger.Error(e);
                Console.ReadKey(true);
            }
            finally
            {
                host.Stop();
            }

            Console.WriteLine("Goodbye");
        }
    }
}
