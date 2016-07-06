using System;
using Nancy.Hosting.Self;

namespace Executable
{
    class Program
    {
        const ushort Port = 64402;
        const string EscapeString = "/Terminate";

        static void Main()
        {
            NancyHost host;

            #region Making new instance of NancyHost
            var uri = new Uri("http://localhost:" + Port + "/");
            var config = new HostConfiguration(); config.UrlReservations.CreateAutomatically = true;

            host = new NancyHost(config, uri);
            #endregion
            #region NancyFX hosting loop
            try
            {
                host.Start();

                Console.Write("Start hosting the Fate/Another ranking system frontend\n" +
                    "\t\"" + uri + "\"\n" +
                    "To stop the hosting, input \"" + EscapeString + "\".\n\n");
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
