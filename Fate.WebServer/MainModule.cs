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
        private static readonly GameSL _gameSl = GameSL.Instance;
        private static readonly PlayerStatSL _playerStatsSl = PlayerStatSL.Instance;
        private static readonly GameListSL _gameListSl = GameListSL.Instance;
        private static readonly GameDetailSL _gameDetailSl = GameDetailSL.Instance;
        private static readonly StatisticsSL _statisticsSl = StatisticsSL.Instance;

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
                return View["Views/GameList.sshtml", _gameListSl.GetGameList()];
            };

            Get["/About"] = Param => View["Views/About.sshtml"];

            Get["/ServantStatistics"] = Param =>
            {
                return View["Views/ServantStatistics.sshtml", _statisticsSl.GetServantStatistics()];
            };

            Get["/PlayerGameBuildDetail/{GameID}/{PlayerName}"] = param =>
            {
                PlayerGameBuildViewModel vm = _gameDetailSl.GetPlayerGameBuild(param.GameID, param.PlayerName);
                return View["Views/PlayerGameBuildDetail.sshtml",vm];
            };

            Get["/Log"] = x =>
            {
                string gameLog = Regex.Replace(_gameSl.GetGameLog(Request.Query.gameId), @"\r\n?|\n", "<br />"); 
                return View["Views/Log.sshtml",gameLog];
            };
            Get["/PlayerStats/{server}/{playerName}"] = param =>
            {
                PlayerStatsPageViewModel summaryData = _playerStatsSl.GetPlayerStatSummary(param.playerName, param.server, int.MaxValue);
                return View["Views/PlayerStatsPage.sshtml", summaryData];
            };
        }
    }
}
