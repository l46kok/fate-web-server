using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Fate.Common.Data;
using Fate.WebServiceLayer;
using FateWebServer.ViewModels;
using Nancy;

namespace Executable
{
    public class MainModule : NancyModule
    {
        private static readonly GameSL _gameSl = new GameSL();
        public MainModule()
        {
            Get["/"] = _ =>
            {
                MainPageViewModel mpVm = new MainPageViewModel
                {
                    CurrentBotTime = DateTime.Now,
                    RecentGameDataList = _gameSl.GetRecentGames(10)
                };
                return View["Views/MainPage.sshtml", mpVm];
            };
            Get["/Log"] = x =>
            {
                string gameLog = Regex.Replace(_gameSl.GetGameLog(Request.Query.gameId), @"\r\n?|\n", "<br />"); 
                return View["Views/Log.sshtml",gameLog];
            };
            Get["/PlayerStats/{server}/{playerName}"] = param =>
            {
                PlayerStatsPageViewModel vm = new PlayerStatsPageViewModel
                {
                    HasFoundUser = false,
                    UserName = param.playerName
                };
                return View["Views/PlayerStatsPage.sshtml", vm];
            };

            /*            Get["/SearchPlayer"] = _ => View["Views/SearchPlayer.html"];
                        Get["/SearchGame"] = _ => View["Views/SearchGame.html"];
                        Get["/Home"] = _ => View["Views/Home.html"];
                        Get["/About"] = _ => View["Views/About.html"];
                        Get["/PlayerStats"] = _ => View["Views/PlayerStats.html"];*/
        }
    }
}
