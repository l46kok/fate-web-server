using System;
using Fate.Common.Utility;
using Fate.DB;
using Nancy.Hosting.Self;
using NLog;

namespace FateWebServer
{
    static class Program
    {
        private const string CMD_TERMINATE = "/Terminate";
        private const string CMD_MAINTENANCE = "/Maintenance";
        private const string CMD_RELOAD_CONFIG = "/ReloadConfig";
        private const string CMD_RESTART = "/Restart";
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
                frsDatabase.InitDatabaseConnection(ConfigHandler.DatabaseServer,
                                             ConfigHandler.DatabasePort,
                                             ConfigHandler.DatabaseUserName,
                                             ConfigHandler.DatabasePassword,
                                             ConfigHandler.DatabaseName);
            }
        }

        private static void RunServer()
        {
            var uri = new Uri("http://localhost:" + ConfigHandler.WebServerPort + "/");
            var config = new HostConfiguration
            {
                UrlReservations = { CreateAutomatically = true },
                AllowChunkedEncoding = false
            };

            var host = new NancyHost(config, uri);
            bool isRestart = false;

            try
            {
                host.Start();

                Console.Write("Fate / Another III Stats Page");

                Console.Write("\n" +
                              "\t\"" + uri + "\"\n" +
                              $"To quit, input {CMD_TERMINATE}\n" +
                              $"To set maintenance mode, input {CMD_MAINTENANCE}\n" +
                              $"To reload configuration, input {CMD_RELOAD_CONFIG}\n" +
                              $"To restart server, input {CMD_RESTART}\n");
                bool terminateServer = false;
                
                while (!terminateServer)
                {
                    Console.Write("> ");
                    string cmd = Console.ReadLine();
                    switch (cmd)
                    {
                        case CMD_TERMINATE:
                            terminateServer = true;
                            break;
                        case CMD_MAINTENANCE:
                            MainBootstrapper.IsMaintenanceMode = !MainBootstrapper.IsMaintenanceMode;
                            _logger.Info(MainBootstrapper.IsMaintenanceMode
                                ? "Starting maintenance mode"
                                : "Resuming web service");
                            break;
                        case CMD_RELOAD_CONFIG:
                            LoadConfig();
                            break;
                        case CMD_RESTART:
                            isRestart = true;
                            terminateServer = true;
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

            if (isRestart)
            {
                Console.WriteLine("Restarting Server");
                // Starts a new instance of the program itself
                System.Diagnostics.Process.Start(PathHandler.GetAssemblyPath());

                // Closes the current process
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Goodbye");
            }
        }
    }
}
