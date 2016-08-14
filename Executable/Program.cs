using System;
using Nancy.Hosting.Self;

namespace FateWebServer
{
    static class Program
    {
        const ushort Port = 64402;
        const string EscapeString = "/Terminate";

        static void Main()
        {
            #region Making new instance of NancyHost
            var uri = new Uri("http://localhost:" + Port + "/");
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
                    "To quit, input \"" + EscapeString + "\".\n\n");
                do Console.Write("> "); while (Console.ReadLine() != EscapeString) ;
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
