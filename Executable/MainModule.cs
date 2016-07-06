using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;

namespace Executable
{
    public class MainModule : NancyModule
    {
        public MainModule()
        {
            Get["/"] = _ => View["Views/MainPage.html"];
            Get["/SearchPlayer"] = _ => View["Views/SearchPlayer.html"];
            Get["/SearchGame"] = _ => View["Views/SearchGame.html"];
            Get["/Home"] = _ => View["Views/Home.html"];
            Get["/About"] = _ => View["Views/About.html"];
            Get["/PlayerStats"] = _ => View["Views/PlayerStats.html"];
        }
    }
}
