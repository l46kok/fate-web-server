using System;
using System.Text.RegularExpressions;
using Fate.Common.Data;
using Fate.WebServiceLayer;
using Fate.WebServiceLayer.ViewModels;
using Nancy;
using Nancy.Linker;

namespace FateWebServer
{
    public class MainModule : NancyModule
    {
        private static readonly GameSL _gameSl = new GameSL();
        private static readonly PlayerStatSL _playerStatsSl = new PlayerStatSL();
        private static readonly GameListSL _gameListSl = GameListSL.Instance;
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

            Post["/Search"] = param => Response.AsRedirect($"/PlayerStats/USEast/{Request.Form.searchPlayerName}");

            Get["/GameList"] = param =>
            {
                GameListData data = _gameListSl.GetGameList();
                return View["Views/GameList.sshtml", data];
            };

            Get["/About"] = Param => View["Views/About.sshtml"];

            Get["/Log"] = x =>
            {
                string gameLog = Regex.Replace(_gameSl.GetGameLog(Request.Query.gameId), @"\r\n?|\n", "<br />"); 
                return View["Views/Log.sshtml",gameLog];
            };
            Get["/PlayerStats/{server}/{playerName}"] = param =>
            {
                PlayerStatsPageViewModel summaryData = _playerStatsSl.GetPlayerStatSummary(param.playerName, param.server);
                return View["Views/PlayerStatsPage.sshtml", summaryData];
            };
        }
    }
}
