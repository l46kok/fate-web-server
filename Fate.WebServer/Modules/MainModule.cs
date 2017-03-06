using System;
using System.Diagnostics.CodeAnalysis;
using Fate.Common.Extension;
using Fate.Common.Utility;
using Fate.WebServiceLayer;
using Fate.WebServiceLayer.ViewModels;
using Nancy;
using Nancy.Linker;

namespace FateWebServer.Modules
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class MainModule : NancyModule
    {
        private static readonly GameSL _gameSl = GameSL.Instance;
        private static readonly PlayerStatSL _playerStatsSl = PlayerStatSL.Instance;
        private static readonly GhostCommSL GhostCommSl = GhostCommSL.Instance;
        private static readonly GameDetailSL _gameDetailSl = GameDetailSL.Instance;
        private static readonly StatisticsSL _statisticsSl = StatisticsSL.Instance;
        private static readonly BanListSL _banListSl = BanListSL.Instance;

        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        public MainModule(IResourceLinker linker)
        {
            Get["/"] = _ =>
            {
                Context.EnableOutputCache(15);
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
                Context.EnableOutputCache(10);
                return View["Views/GameList.sshtml", GhostCommSl.GetGameList()];
            };

            Get["/About"] = Param => View["Views/About.sshtml"];

            Get["/ServantStatistics"] = Param =>
            {
                Context.EnableOutputCache(30);
                return View["Views/ServantStatistics.sshtml", _statisticsSl.GetServantStatistics()];
            };

            Get["/LeaderBoards"] = Param =>
            {
                Context.EnableOutputCache(30);
                return View["Views/LeaderBoards.sshtml"];
            };

            Get["/Downloads"] = Param => View["Views/Downloads.sshtml", ConfigHandler.MapName];

            Get["/PlayerGameBuildDetail/{GameID}/{PlayerName}"] = param =>
            {
                PlayerGameBuildViewModel vm = _gameDetailSl.GetPlayerGameBuild(param.GameID, param.PlayerName);
                return View["Views/PlayerGameBuildDetail.sshtml",vm];
            };

            Get["/Log"] = x =>
            {
                string gameLog = _gameSl.GetGameLog(Request.Query.gameId);
                return View["Views/Log.sshtml",gameLog];
            };
            Get["/PlayerStats/{server}/{playerName}"] = param =>
            {
                PlayerStatsPageViewModel summaryData = _playerStatsSl.GetPlayerStatSummary(param.playerName, param.server, int.MaxValue);
                return View["Views/PlayerStatsPage.sshtml", summaryData];
            };

            Get["/Maintenance"] = param => View["Views/Maintenance.sshtml"];

            Get["/Login"] = param => View["Views/Login.sshtml", new LoginViewModel()];

            Get["/BanList"] = param => View["Views/BanList.sshtml", _banListSl.GetBannedPlayers()];
        }
    }
}
