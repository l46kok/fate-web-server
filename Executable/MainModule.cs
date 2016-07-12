using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Fate.Common.Data;
using Fate.WebServiceLayer;
using Fate.WebServiceLayer.ViewModels;
using Nancy;
using Nancy.Linker;

namespace Executable
{
    public class MainModule : NancyModule
    {
        private static readonly GameSL _gameSl = new GameSL();
        private static readonly PlayerStatSL _playerStatsl = new PlayerStatSL();
        public MainModule(IResourceLinker linker)
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

            Post["/Search"] = param =>
            {
                return Response.AsRedirect($"/PlayerStats/USEast/{Request.Form.searchPlayerName}");
            };


            Get["/Log"] = x =>
            {
                string gameLog = Regex.Replace(_gameSl.GetGameLog(Request.Query.gameId), @"\r\n?|\n", "<br />"); 
                return View["Views/Log.sshtml",gameLog];
            };
            Get["/PlayerStats/{server}/{playerName}"] = param =>
            {
                PlayerStatsPageViewModel summaryData = _playerStatsl.GetPlayerStatSummary(param.playerName, param.server);
                return View["Views/PlayerStatsPage.sshtml", summaryData];
            };
        }
    }
}
